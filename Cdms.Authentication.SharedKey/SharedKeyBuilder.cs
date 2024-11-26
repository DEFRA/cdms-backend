using System.Security.Cryptography;
using System.Text;

namespace Cdms.Authentication.SharedKey;

public static class SharedKeyBuilder
{
    public static string CreateHttpHeader(string clientId, string clientSecret, string hashKey, TimeProvider timeProvider = default!)
    {
        return $"SharedKey {CreateEncodedValue(clientId, clientSecret, hashKey, timeProvider)}";
    }

    public static string CreateEncodedValue(string clientId, string clientSecret, string hashKey, TimeProvider timeProvider = null!)
    {
        var tp = timeProvider == null ? TimeProvider.System : timeProvider;
        var timestamp = tp.GetUtcNow().ToUnixTimeSeconds();
        string header = $"{clientId}_{timestamp}:{CreateHashSecret(timestamp, clientSecret, hashKey)}";
        var textBytes = Encoding.UTF8.GetBytes(header);
        return Convert.ToBase64String(textBytes);
    }

    public static string CreateHashSecret(long timestamp, string clientSecret, string hashKey)
    {
        var encoding = new UTF8Encoding(false, true);
        var secret = $"{clientSecret}_{timestamp}";

        HashAlgorithm hashAlgorithm = new HMACSHA256(encoding.GetBytes(hashKey));
        byte[] messageBuffer = encoding.GetBytes(secret);
        var hash = hashAlgorithm.ComputeHash(messageBuffer);
        return BitConverter.ToString(hash);
    }
}