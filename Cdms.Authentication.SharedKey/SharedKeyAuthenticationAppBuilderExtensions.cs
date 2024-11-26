using Cdms.Authentication.SharedKey.KeyStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Cdms.Authentication.SharedKey
{
    /// <summary>
    /// Extension methods to add Basic authentication capabilities to an HTTP application pipeline.
    /// </summary>
    public static class SharedKeyAuthenticationAppBuilderExtensions
    {
        public static AuthenticationBuilder AddSharedKey(this AuthenticationBuilder builder, Action<SharedKeyAuthenticationOptions> configureOptions)
            => builder.AddBasic(SharedKeyAuthenticationDefaults.AuthenticationScheme, configureOptions);

        public static AuthenticationBuilder AddBasic(
            this AuthenticationBuilder builder,
            string authenticationScheme,
            Action<SharedKeyAuthenticationOptions> configureOptions)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Services.AddSingleton<IAuthKeyStore, InMemoryAuthKeyStore>();
            return builder.AddScheme<SharedKeyAuthenticationOptions, SharedKeyAuthenticationHandler>(authenticationScheme, configureOptions);
        }
    }
}


