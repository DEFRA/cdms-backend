namespace Cdms.Authentication.SharedKey.KeyStore
{
    public interface IAuthKeyStore
    {
        Task<bool> TryGet(string clientId, out ClientKeyRecord record);

        Task Add(ClientKeyRecord record);
    }
}
