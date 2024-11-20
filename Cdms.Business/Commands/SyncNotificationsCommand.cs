using Cdms.BlobService;
using Cdms.SensitiveData;
using Cdms.SyncJob;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SlimMessageBus;

namespace Cdms.Business.Commands
{
    public class SyncNotificationsCommand : SyncCommand
    {
        public string[] ChedTypes { get; set; } = [];
        public override string Resource => "ImportNotification";

        public string[] BlobFiles { get; set; } = [];


        internal class Handler(
            SyncMetrics syncMetrics,
            IPublishBus bus,
            ILogger<SyncNotificationsCommand> logger,
            ISensitiveDataSerializer sensitiveDataSerializer,
            IBlobService blobService,
            IOptions<BusinessOptions> businessOptions,
            ISyncJobStore syncJobStore)
            : SyncCommand.Handler<SyncNotificationsCommand>(syncMetrics, bus, logger, sensitiveDataSerializer,
                blobService, syncJobStore)
        {
            public override async Task Handle(SyncNotificationsCommand request, CancellationToken cancellationToken)
            {
                var rootFolder = string.IsNullOrEmpty(request.RootFolder)
                    ? businessOptions.Value.DmpBlobRootFolder
                    : request.RootFolder;

                if (request.BlobFiles.Any())
                {
                    await SyncBlobs<Types.Ipaffs.ImportNotification>(request.SyncPeriod, "NOTIFICATIONS",
                        request.JobId,
                        request.BlobFiles.Select(x => $"{rootFolder}/IPAFFS/{x}").ToArray());
                    return;
                }

                var chedTypesToSync = new List<string>();
                

                if (!request.ChedTypes.Any() || request.ChedTypes.Contains("CHEDA"))
                {
                    chedTypesToSync.Add($"{rootFolder}/IPAFFS/CHEDA");
                }

                if (!request.ChedTypes.Any() || request.ChedTypes.Contains("CHEDD"))
                {
                    chedTypesToSync.Add($"{rootFolder}/IPAFFS/CHEDD");
                }

                if (!request.ChedTypes.Any() || request.ChedTypes.Contains("CHEDP"))
                {
                    chedTypesToSync.Add($"{rootFolder}/IPAFFS/CHEDP");
                }

                if (!request.ChedTypes.Any() || request.ChedTypes.Contains("CHEDPP"))
                {
                    chedTypesToSync.Add($"{rootFolder}/IPAFFS/CHEDPP");
                }

                await SyncBlobPaths<Types.Ipaffs.ImportNotification>(request.SyncPeriod, "NOTIFICATIONS",
                    request.JobId,
                    chedTypesToSync.ToArray());
            }
        }
    }
}