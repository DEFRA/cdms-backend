using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.FileProviders;

namespace Cdms.Analytics.Tests.Helpers;

public class DummyWebHostEnvironment : IWebHostEnvironment
{
    public string EnvironmentName { get; set; } = null!;
    public string ApplicationName { get; set; } = null!;
    public string ContentRootPath { get; set; } = null!;
    public IFileProvider ContentRootFileProvider { get; set; } = null!;
    public string WebRootPath { get; set; } = null!;
    public IFileProvider WebRootFileProvider { get; set; } = null!;
}