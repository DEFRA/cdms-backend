using FluentAssertions;
using Xunit;

namespace TestDataGenerator.Tests;

public class Test1
{
    [Fact]
    public void Testing1()
    {
        true.Should().Be(true);
    }
}