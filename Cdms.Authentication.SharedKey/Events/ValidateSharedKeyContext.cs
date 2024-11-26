using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Cdms.Authentication.SharedKey.Events
{
    public class ValidateSharedKeyContext : ResultContext<SharedKeyAuthenticationOptions>
    {
        public ValidateSharedKeyContext(
            HttpContext context,
            AuthenticationScheme scheme,
            SharedKeyAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public string KeyId { get; set; } = string.Empty;

        public long Timestamp { get; set; }

        public string Password { get; set; }
    }
}
