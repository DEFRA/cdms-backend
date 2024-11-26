using Cdms.Authentication.SharedKey.Events;
using Cdms.Authentication.SharedKey.KeyStore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Encodings.Web;

namespace Cdms.Authentication.SharedKey
{
    public class SharedKeyAuthenticationHandler(
        IOptionsMonitor<SharedKeyAuthenticationOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IAuthKeyStore authKeyStore)
        : AuthenticationHandler<SharedKeyAuthenticationOptions>(options, logger, encoder)
    {
        private readonly UTF8Encoding _utf8ValidatingEncoding = new UTF8Encoding(false, true);

        /// <summary>
        /// The handler calls methods on the events which give the application control at certain points where processing is occurring.
        /// If it is not provided a default instance is supplied which does nothing when the methods are called.
        /// </summary>
        protected new SharedKeyAuthenticationEvents Events
        {
            get => (SharedKeyAuthenticationEvents)base.Events;
            set => base.Events = value;
        }

        protected override Task<object> CreateEventsAsync() => Task.FromResult<object>(new SharedKeyAuthenticationEvents());

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            string authorizationHeader = Request.Headers.Authorization;


            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            // Exact match on purpose, rather than using string compare
            // asp.net request parsing will always trim the header and remove trailing spaces
            if (SharedKeyAuthenticationDefaults.AuthenticationScheme == authorizationHeader)
            {
                const string noCredentialsMessage = "Authorization scheme was SharedKey but the header had no credentials.";
                Logger.LogInformation(noCredentialsMessage);
                return AuthenticateResult.Fail(noCredentialsMessage);
            }

            if (!authorizationHeader.StartsWith(SharedKeyAuthenticationDefaults.AuthenticationScheme + ' ', StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.NoResult();
            }

            string encodedCredentials = authorizationHeader.Substring(SharedKeyAuthenticationDefaults.AuthenticationScheme.Length).Trim();

            try
            {
                string decodedCredentials = string.Empty;
                byte[] base64DecodedCredentials;
                try
                {
                    base64DecodedCredentials = Convert.FromBase64String(encodedCredentials);
                }
                catch (FormatException)
                {
                    const string failedToDecodeCredentials = "Cannot convert credentials from Base64.";
                    Logger.LogInformation(failedToDecodeCredentials);
                    return AuthenticateResult.Fail(failedToDecodeCredentials);
                }

                try
                {
                    decodedCredentials = _utf8ValidatingEncoding.GetString(base64DecodedCredentials);
                }
                catch (Exception ex)
                {
                    const string failedToDecodeCredentials = "Cannot build credentials from decoded base64 value, exception {ex.Message} encountered.";
                    Logger.LogInformation(failedToDecodeCredentials, ex.Message);
                    return AuthenticateResult.Fail(ex.Message);
                }

                var delimiterIndex = decodedCredentials.IndexOf(":", StringComparison.OrdinalIgnoreCase);
                if (delimiterIndex == -1)
                {
                    const string missingDelimiterMessage = "Invalid credentials, missing delimiter.";
                    Logger.LogInformation(missingDelimiterMessage);
                    return AuthenticateResult.Fail(missingDelimiterMessage);
                }

                var username = decodedCredentials.Substring(0, delimiterIndex);
                var sa = username.Split("_");
                var password = decodedCredentials.Substring(delimiterIndex + 1);

                var validateCredentialsContext = new ValidateSharedKeyContext(Context, Scheme, Options)
                {
                    KeyId = sa[0],
                    Timestamp = long.Parse(sa[1]),
                    Password = password
                };

                var now = DateTimeOffset.UtcNow;
                var headerTime = DateTimeOffset.FromUnixTimeSeconds(validateCredentialsContext.Timestamp);

                if (headerTime > now)
                {
                    const string timestampInFuture = "The Timestamp in header is in the future";
                    Logger.LogInformation(timestampInFuture);
                    return AuthenticateResult.Fail(timestampInFuture);
                }

                if (now.Subtract(headerTime) > options.CurrentValue.MaximumMessageValidity)
                {
                    const string requestOutsideValidityRange = "Request is outside of validity range.";
                    Logger.LogInformation(requestOutsideValidityRange);
                    return AuthenticateResult.Fail(requestOutsideValidityRange);
                }



                await Events.ValidateCredentials(validateCredentialsContext, authKeyStore);

                if (validateCredentialsContext.Result != null &&
                    validateCredentialsContext.Result.Succeeded)
                {
                    var ticket = new AuthenticationTicket(validateCredentialsContext.Principal, Scheme.Name);
                    return AuthenticateResult.Success(ticket);
                }

                if (validateCredentialsContext.Result != null &&
                    validateCredentialsContext.Result.Failure != null)
                {
                    return AuthenticateResult.Fail(validateCredentialsContext.Result.Failure);
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                var authenticationFailedContext = new SharedKeyAuthenticationFailedContext(Context, Scheme, Options)
                {
                    Exception = ex
                };

                await Events.AuthenticationFailed(authenticationFailedContext).ConfigureAwait(true);

                if (authenticationFailedContext.Result != null)
                {
                    return authenticationFailedContext.Result;
                }

                throw;
            }
        }

        protected override Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            Response.StatusCode = 401;
            return Task.CompletedTask;
        }
    }
}