namespace Cdms.Authentication.SharedKey.Tests
{
    public class TestTimeProvider(DateTimeOffset dateTimeOffset) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow()
        {
            return dateTimeOffset;
        }
    }
}
