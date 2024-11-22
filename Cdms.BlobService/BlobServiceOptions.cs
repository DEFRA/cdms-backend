using System.ComponentModel.DataAnnotations;
using Cdms.Azure;
using Cdms.Common.Extensions;

namespace Cdms.BlobService;

public class BlobServiceOptions : IAzureConfig
{
    public const string SectionName = nameof(BlobServiceOptions);

    [Required] public string DmpBlobContainer { get; set; } = null!;

    [Required] public string DmpBlobUri { get; set; } = null!;
    [Required] public string AzureClientId { get; set; } = null!;
    [Required] public string AzureTenantId { get; set; } = null!;
    [Required] public string AzureClientSecret { get; set; } = null!;

    public bool CacheReadEnabled { get; set; }
    public bool CacheWriteEnabled { get; set; }
    
    public string CachePath { get; set; } = ".synchroniser-cache";
    
    public int Retries { get; set; } = 3;

    public int Timeout { get; set; } = 10;
    
}