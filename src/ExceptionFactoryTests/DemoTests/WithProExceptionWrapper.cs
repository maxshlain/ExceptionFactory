using FluentAssertions;
using Xunit.Abstractions;

namespace ExceptionFactoryTests.DemoTests;

public class WithProExceptionWrapper
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private const string ExpectedMessageWithProblemDetails =
        @"Attempted to divide by zero.
Called: () => ThrowingClass.Divide(1, 0)
 With facts: {""x"":1,""y"":0}
";

    public WithProExceptionWrapper(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void WrappedThrowsExceptionWithDetails()
    {
        // ARRANGE
        var throwing = new ThrowingClass();
        var actor = new DivideByZeroActorWithProExceptionWrapper(throwing);
        var sut = new Controller1(actor);
        Exception? caught = null;
        
        // ACT
        try
        {
            sut.ExecuteBusinessLogic();
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
}
