namespace CdmsBackend.Authentication
{
	public class ClientCredentialsManager(IClientCredentialsStore clientCredentialsStore) : IClientCredentialsManager
	{
		public async Task<bool> IsValid(string clientId, string clientSecret)
		{
			var actualSecret = await clientCredentialsStore.GetClientSecret(clientId);
			return clientSecret.Equals(actualSecret);
		}
	}
}