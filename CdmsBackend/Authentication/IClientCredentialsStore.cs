namespace CdmsBackend.Authentication
{
	public interface IClientCredentialsStore
	{
		Task<string?> GetClientSecret(string clientId);
	}
}