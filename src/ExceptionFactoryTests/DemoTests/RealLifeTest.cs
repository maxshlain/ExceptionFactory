using FluentAssertions;
using Xunit.Abstractions;

namespace ExceptionFactoryTests.DemoTests;

public class RealLifeTest
{
    private readonly ITestOutputHelper _testOutputHelper;

    public RealLifeTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void ThrowingThrowsUselessException()
    {
        // ARRANGE
        ThrowingClass throwing = new ThrowingClass();
        Actor actor = new Actor(throwing);
        IController sut = new Controller0(actor);

        // ACT
        Exception? caught = null;
        try
        {
            sut.ExecuteBusinessLogic();
        }
        catch (Exception uselessException)
        {
            _testOutputHelper.WriteLine(uselessException.ToString());
            caught = uselessException;
            caught.Message.Should().Be("Attempted to divide by zero.");
        }
        
        // ASSERT
        caught.Should().NotBeNull();
    }
}
