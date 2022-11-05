using FluentAssertions;
using LoggingImplDefault;
using Xunit.Abstractions;

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

[Log]
public interface ICalculator
{
    public int Add(int x, int y);
    public int Divide(int x, int y);
}

public class RealCalculator : ICalculator 
{
    public int Add(int x, int y)
    {
        return x + y;
    }

    public int Divide(int x, int y)
    {
        return x / y;
    }
}

public class LogGeneratorTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public LogGeneratorTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void SourceGenInterceptedThrowsExceptionWithDetails()
    {
        // ARRANGE
        ICalculator sut = new RealCalculator().WithLogging();
        Exception? caught = null;
        
        // ACT
        try
        {
            sut.Divide(1, 0);
        }
        catch (Exception exceptionWithProblemDetails)
        {
            caught = exceptionWithProblemDetails;
            _testOutputHelper.WriteLine(exceptionWithProblemDetails.ToString());
        }
        
        // ASSERT
        caught.Should().NotBeNull();
        caught?.Message.Should().Be(ExpectedMessageWithProblemDetails);
    }
    private const string ExpectedMessageWithProblemDetails =
        @"Attempted to divide by zero.
Called: int ExceptionFactoryTests.SourceGen.ICalculator.Divide(System.Int32 x, System.Int32 y)
 With facts: {""x"":1,""y"":0}
";
}
