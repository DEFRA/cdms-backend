using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Cdms.Authentication.SharedKey.Events
{
    public class SharedKeyAuthenticationFailedContext : ResultContext<SharedKeyAuthenticationOptions>
    {
        public SharedKeyAuthenticationFailedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            SharedKeyAuthenticationOptions options)
            : base(context, scheme, options)
        {
        }

        public Exception Exception { get; set; }
    }
}
