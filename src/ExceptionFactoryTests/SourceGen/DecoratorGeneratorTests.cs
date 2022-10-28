using FluentAssertions;

namespace ExceptionFactoryTests.SourceGen;

public class DecoratorGeneratorTests
{
    [Fact]
    public void ShouldSayHi()
    {
        // ARRANGE
        var sut = new FunNs.Say();
        
        // ACT
        var fromTest = sut.Hello();

        // ASSERT
        fromTest.Should().Be("Hello");
    }
}
