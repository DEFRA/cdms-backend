using Cdms.Authentication.SharedKey.Events;
using Cdms.Authentication.SharedKey.KeyStore;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using Microsoft.Extensions.WebEncoders.Testing;
using Xunit;

namespace Cdms.Authentication.SharedKey.Tests;

public class SharedKeyAuthenticationHandlerTests
{
    [Fact]
    public async Task GivenNoAuthorizationHeader_ThenAuthenticationShouldFail()
    {
        var handler = CreateHandler();

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), new DefaultHttpContext());

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenAuthorizationHeaderWithNoCredentials_ThenAuthenticationShouldFail()
    {
        var handler = CreateHandler();

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues(SharedKeyAuthenticationDefaults.AuthenticationScheme);

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenAuthorizationHeaderThatDoesNotStartWithSharedKey_ThenAuthenticationShouldFail()
    {
        var handler = CreateHandler();

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues("Basic");

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenAuthorizationHeaderThatIsNotBase64Encoded_ThenAuthenticationShouldFail()
    {
        var handler = CreateHandler();

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues("SharedKey TestValue:NotEncoded");

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }


    [Fact]
    public async Task GivenValidAuthorizationHeader_ThenAuthenticationShouldSucceed()
    {
        var options = new SharedKeyAuthenticationOptions()
        {
            Events = new SharedKeyAuthenticationEvents()
        };
        var authKeyStore = new InMemoryAuthKeyStore();
        authKeyStore.Add(new ClientKeyRecord() { Id = "TestClient", Secret = "TestSecret", SharedKey = "TestHash" });
        var handler = CreateHandler(options, authKeyStore);

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues(SharedKeyBuilder.CreateHttpHeader("TestClient", "TestSecret", "TestHash"));

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeTrue();
    }

    [Fact]
    public async Task GivenValidAuthorizationHeader_WhenClientIdDoesNotExist_ThenAuthenticationShouldFail()
    {
        var options = new SharedKeyAuthenticationOptions()
        {
            Events = new SharedKeyAuthenticationEvents()
        };

        var handler = CreateHandler(options);

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues(SharedKeyBuilder.CreateHttpHeader("TestClient", "TestSecret", "TestHash"));

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }


    [Fact]
    public async Task GivenValidAuthorizationHeader_ButTimestampIsInFuture_ThenAuthenticationShouldSFail()
    {
        var handler = CreateHandler();

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues(SharedKeyBuilder.CreateHttpHeader("TestClient", "TestSecret", "TestHash",
                new TestTimeProvider(DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(30)))));

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }

    [Fact]
    public async Task GivenValidAuthorizationHeader_ButTimestampOlderThan15Mins_ThenAuthenticationShouldSFail()
    {
        var handler = CreateHandler();

        var context = new DefaultHttpContext();
        context.Request.Headers.Authorization =
            new StringValues(SharedKeyBuilder.CreateHttpHeader("TestClient", "TestSecret", "TestHash",
                new TestTimeProvider(DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(-17)))));

        await handler.InitializeAsync(new SharedKeyAuthenticationScheme(), context);

        var authResult = await handler.AuthenticateAsync();

        authResult.Succeeded.Should().BeFalse();
    }

    private static SharedKeyAuthenticationHandler CreateHandler(SharedKeyAuthenticationOptions authOptions = null, IAuthKeyStore authKeyStore = null)
    {
        var options = new TestOptionsMonitor<SharedKeyAuthenticationOptions>(authOptions == null ? new SharedKeyAuthenticationOptions() : authOptions);
        return new SharedKeyAuthenticationHandler(options, NullLoggerFactory.Instance, new UrlTestEncoder(), authKeyStore == null ? new InMemoryAuthKeyStore() : authKeyStore);
    }
}