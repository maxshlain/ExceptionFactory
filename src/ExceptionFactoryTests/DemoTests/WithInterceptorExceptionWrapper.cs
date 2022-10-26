using ExceptionFactoryCore;
using FluentAssertions;
using Xunit.Abstractions;

namespace ExceptionFactoryTests.DemoTests;

public class WithInterceptorExceptionWrapper
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private const string ExpectedMessageWithProblemDetails =
        @"Attempted to divide by zero.
Called: Double Divide(Int32, Int32)
 With facts: [1,0]
";

    public WithInterceptorExceptionWrapper(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void InterceptedThrowsExceptionWithDetails()
    {
        // ARRANGE
        var throwing = new ThrowingClass();
        var throwingWithInterceptor = Pro.Exceptions.InterceptAll(throwing);
        var actor = new Actor(throwingWithInterceptor);
        var sut = new Controller0(actor);
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
