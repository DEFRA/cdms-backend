using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Cdms.BlobService;

public class CachingBlobService(
    [FromKeyedServices("base")] IBlobService blobService,
    ILogger<CachingBlobService> logger,
    IOptions<BlobServiceOptions> options
) : IBlobService
{
    public Task<Status> CheckBlobAsync(int timeout = default, int retries = default)
    {
        return blobService.CheckBlobAsync();
    }

    public Task<Status> CheckBlobAsync(string uri, int timeout = default, int retries = default)
    {
        return blobService.CheckBlobAsync(uri, timeout, retries);
    }

    public async IAsyncEnumerable<IBlobItem> GetResourcesAsync(string prefix, CancellationToken cancellationToken)
    {
        var path = Path.GetFullPath($"{options.Value.CachePath}/{prefix}");
        logger.LogInformation($"Scanning disk {path}");

        if (Directory.Exists(path))
        {
            logger.LogInformation($"Folder {path}exists, looking for files.");  
            foreach (string f in Directory.GetFiles(path))
            {
                logger.LogInformation($"Found file {f}");
                yield return new SynchroniserBlobItem() { Name = f };
            }

            foreach (string d in Directory.GetDirectories(path))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    logger.LogInformation($"Found file {f}");
                    yield return new SynchroniserBlobItem() { Name = f };
                }
            }
        }
        else{
            logger.LogWarning($"Cannot scan folder {path} as it doesn't exist.");    
        }
    }

    public Task<string> GetResource(IBlobItem item, CancellationToken cancellationToken)
    {
        logger.LogInformation($"GetResource {item.Name}");
        return blobService.GetResource(item, cancellationToken);
    }
}