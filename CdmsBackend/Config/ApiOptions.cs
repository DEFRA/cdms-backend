using System.ComponentModel.DataAnnotations;
using Cdms.SensitiveData;
using Cdms.Common.Extensions;
using Microsoft.Extensions.Options;

namespace CdmsBackend.Config;

public class ApiOptions
{
    public static readonly string SectionName = nameof(ApiOptions);

    public bool EnableManagement { get; set; } = default!;

    [ConfigurationKeyName("CDP_HTTPS_PROXY")]
    public string? CdpHttpsProxy { get; set; }

    // This is used by the azure library when connecting to auth related services
    // when connecting to blob storage
    [ConfigurationKeyName("HTTPS_PROXY")]
    public string? HttpsProxy { get; set; }

    public class Validator(ILogger<ApiOptions.Validator> logger) : IValidateOptions<ApiOptions>
    { 
        public ValidateOptionsResult Validate(string? name, ApiOptions options)
        {
            bool valid = !options.CdpHttpsProxy.HasValue() || options.HttpsProxy.HasValue();
            return valid
                ? ValidateOptionsResult.Success
                : ValidateOptionsResult.Fail("either CdpHttpsProxy or HttpsProxy must be set");
        }
    }
    
    /// <summary>
    /// Validates that if we have CDP_HTTPS_PROXY we also have HTTPS_PROXY
    /// I'm sure this can be written more concisely, there are tests
    /// </summary>
    /// <returns></returns>
    public bool Validate(ILogger<ApiOptions.Validator> logger)
    {
        var valid = !CdpHttpsProxy.HasValue() || HttpsProxy.HasValue();

        if (!valid)
        {
            logger.LogError("If CDP_HTTPS_PROXY is set HTTPS_PROXY must also be set.");
        }
        if (valid && CdpHttpsProxy.HasValue())
        {
            valid = CdpHttpsProxy!.StartsWith("http");
            
            if (!valid)
            {
                logger.LogError("If CDP_HTTPS_PROXY is set, it must start with protocol");
            }
        }
        
        if (valid && HttpsProxy.HasValue())
        {
            valid = !HttpsProxy!.StartsWith("http");
            
            if (!valid)
            {
                logger.LogError("If HTTPS_PROXY is set HTTPS_PROXY it must not start with protocol");
            }
        }

        return valid;
    }
    
}