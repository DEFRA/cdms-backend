using System.Text.Json.Nodes;
using Cdms.SensitiveData;
using Cdms.Types.Ipaffs;
using FluentAssertions;
using Json.Path;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Xunit;

namespace CdmsBackend.IntegrationTests;

public class SensitiveDataTests
{
    [Fact]
    public void WhenIncludeSensitiveData_RedactedShouldBeSameAsJson()
    {
        var filePath = "../../../Fixtures/SmokeTest/IPAFFS/CHEDA/CHEDA_GB_2024_1041389-ee0e6fcf-52a4-45ea-8830-d4553ee70361.json";
        string json =
            File.ReadAllText(filePath);
        SensitiveDataOptions options = new SensitiveDataOptions { Getter = s => "TestRedacted", Include = true };
        var serializer = new SensitiveDataSerializer(Options.Create(options), NullLogger<SensitiveDataSerializer>.Instance);

        var result = serializer.RedactRawJson(json, typeof(ImportNotification));

        JsonNode.DeepEquals(JsonNode.Parse(json), JsonNode.Parse(result)).Should().BeTrue();

    }

    [Fact]
    public void WhenIncludeSensitiveData_RedactedShouldBeDifferentJson()
    {
        var filePath = "../../../Fixtures/SmokeTest/IPAFFS/CHEDA/CHEDA_GB_2024_1041389-ee0e6fcf-52a4-45ea-8830-d4553ee70361.json";
        string json =
            File.ReadAllText(filePath);
        SensitiveDataOptions options = new SensitiveDataOptions { Getter = s => "TestRedacted", Include = false };
        var serializer = new SensitiveDataSerializer(Options.Create(options), NullLogger<SensitiveDataSerializer>.Instance);

        var result = serializer.RedactRawJson(json, typeof(ImportNotification));

        JsonNode.DeepEquals(JsonNode.Parse(json), JsonNode.Parse(result)).Should().BeFalse();
        result.Should().Contain("TestRedacted");
        var jsonPath = JsonPath.Parse("$.partOne.personResponsible.address[0]");
        var addressResult = jsonPath.Evaluate(JsonNode.Parse(result));
        addressResult.Matches.Should().HaveCountGreaterOrEqualTo(1);
        addressResult.Matches[0].Value?.GetValue<string>().Should().Be("TestRedacted");

    }
}