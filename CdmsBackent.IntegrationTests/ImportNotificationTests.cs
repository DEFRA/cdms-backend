using Cdms.Business.Commands;
using CdmsBackend.IntegrationTests.Helpers;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace CdmsBackend.IntegrationTests;



[Trait("Category", "Integration")]
public class ImportNotificationTests(IntegrationTestsApplicationFactory factory, ITestOutputHelper testOutputHelper)
    : IntegrationTests(factory, testOutputHelper, nameof(ImportNotificationTests)),
        IClassFixture<IntegrationTestsApplicationFactory>, IAsyncLifetime
{
    public async Task InitializeAsync()
    {
        await Factory.ClearDb(Client);
    }

    [Fact]
    public async Task WhenProcessingTheSameImportingNotificationTwice_AndTimeIsToMicrosecond_ThenImportNotificationShouldBeSkipped()
    {
        // Arrange
        var command = new SyncNotificationsCommand()
        {
            SyncPeriod = SyncPeriod.All, RootFolder = "DuplicateAuditEntries",
            ChedTypes = ["CHEDA"]
        };

        //Act
        await MakeSyncNotificationsRequest(command);
        await MakeSyncNotificationsRequest(command);
        await Task.Delay(500);

        // Assert
        var importNotification = await factory.GetDbContext().Notifications.Find("CHEDA.GB.2024.1041389");
        importNotification.AuditEntries.Count.Should().Be(1);
    }



    public Task DisposeAsync()
    {
        return Task.CompletedTask;
    }
}