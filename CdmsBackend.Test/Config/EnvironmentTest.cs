using Microsoft.AspNetCore.Builder;

namespace CdmsBackend.Test.Config;

public class EnvironmentTest
{

   [Fact]
   public void IsNotDevModeByDefault()
   {
      var _builder = WebApplication.CreateBuilder();

      var isDev = CdmsBackend.Config.Environment.IsDevMode(_builder);

      Assert.False(isDev);
   }
}
