using Cdms.Authentication.SharedKey.Events;
using Microsoft.AspNetCore.Authentication;

namespace Cdms.Authentication.SharedKey
{
    public class SharedKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
        public TimeSpan MaximumMessageValidity { get; set; } = new TimeSpan(0, 15, 0);

        public new SharedKeyAuthenticationEvents Events

        {
            get => (SharedKeyAuthenticationEvents)base.Events;

            set => base.Events = value;
        }
    }
}
