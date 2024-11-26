namespace CdmsBackend.Authentication
{
	public class ClientCredentialsStore(IConfiguration configuration) : IClientCredentialsStore
	{
		public Task<string?> GetClientSecret(string clientId)
		{
			return Task.FromResult(configuration.GetValue<string>($"AuthKeyStore:{clientId}_Secret"));
		}
	}
}
