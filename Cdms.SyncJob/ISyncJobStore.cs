namespace Cdms.SyncJob;

public interface ISyncJobStore
{
    List<SyncJob> GetJobs();

    SyncJob? GetJob(Guid id);

    SyncJob CreateJob(Guid id, string timespan, string resource);

    void ClearSyncJobs();
}