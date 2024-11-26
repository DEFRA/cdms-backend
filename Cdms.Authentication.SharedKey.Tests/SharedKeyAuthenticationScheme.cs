using Microsoft.AspNetCore.Authentication;

namespace Cdms.Authentication.SharedKey.Tests;

public class SharedKeyAuthenticationScheme() : AuthenticationScheme(
    SharedKeyAuthenticationDefaults.AuthenticationScheme, SharedKeyAuthenticationDefaults.AuthenticationScheme,
    typeof(SharedKeyAuthenticationHandler));