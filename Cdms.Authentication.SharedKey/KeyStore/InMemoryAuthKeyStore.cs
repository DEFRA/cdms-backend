namespace Cdms.Authentication.SharedKey.KeyStore;

public class InMemoryAuthKeyStore : IAuthKeyStore
{
    private Dictionary<string, ClientKeyRecord> clientKeys = new()
    {
        {"localdev", new ClientKeyRecord() { Id = "localdev", Secret = "localdev", SharedKey = "localdev"}}
    };

    public Task Add(ClientKeyRecord record)
    {
        clientKeys.Add(record.Id, record);
        return Task.CompletedTask;
    }

    Task<bool> IAuthKeyStore.TryGet(string clientId, out ClientKeyRecord record)
    {
        return Task.FromResult(clientKeys.TryGetValue(clientId, out record!));
    }
}