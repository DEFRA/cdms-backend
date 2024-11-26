using Cdms.Authentication.SharedKey.KeyStore;
using System.Security.Claims;

namespace Cdms.Authentication.SharedKey.Events
{
    /// <summary>
    /// This default implementation of the IBasicAuthenticationEvents may be used if the
    /// application only needs to override a few of the interface methods.
    /// This may be used as a base class or may be instantiated directly.
    /// </summary>
    public class SharedKeyAuthenticationEvents
    {
        /// <summary>
        /// A delegate assigned to this property will be invoked when the authentication handler fails and encounters an exception.
        /// </summary>
        public Func<SharedKeyAuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// A delegate assigned to this property will be invoked when the credentials need validation.
        /// </summary>
        /// <remarks>
        /// You must provide a delegate for this property for authentication to occur.
        /// In your delegate you should construct an authentication principal from the user details,
        /// attach it to the context.Principal property and finally call context.Success();
        /// </remarks>
        public Func<ValidateSharedKeyContext, IAuthKeyStore, Task> OnValidateCredentials { get; set; } = async (context, authKeyStore) =>
        {
            if (!await authKeyStore.TryGet(context.KeyId, out var clientKeyRecord))
            {
                context.Fail("Client Id not found");
                return;
            }
            var expectedPassword = SharedKeyBuilder.CreateHashSecret(context.Timestamp, clientKeyRecord.Secret, clientKeyRecord.SharedKey);
            if (context.Password != expectedPassword)
            {
                context.Fail("Invalid authentication header");
                return;
            }

            var claims = new[]
            {
                new Claim("keyId", context.KeyId, ClaimValueTypes.String, context.Options.ClaimsIssuer)
            };
            context.Principal = new ClaimsPrincipal(new ClaimsIdentity(claims, context.Scheme.Name));
            context.Success();
        };

        public virtual Task AuthenticationFailed(SharedKeyAuthenticationFailedContext context) => OnAuthenticationFailed(context);

        public virtual Task ValidateCredentials(ValidateSharedKeyContext context, IAuthKeyStore authKeyStore) => OnValidateCredentials(context, authKeyStore);
    }
}
