using ExceptionFactoryCore;

namespace ExceptionFactoryTests.DemoTests;

public class DivideByZeroActorWithProExceptionWrapper
{
    private readonly ThrowingClass _throwing;

    public DivideByZeroActorWithProExceptionWrapper(ThrowingClass throwing)
    {
        _throwing = throwing;
    }
    
    public void Act()
    {
        int x = 1;
        int y = 0;
        

        Pro.Exceptions.ExecuteWithFacts(
            () => _throwing.Divide(x, y),
            () => new { x, y }
        );
    }
}

public class Actor
{
    private readonly ThrowingClass _throwing;

    public Actor(ThrowingClass throwing)
    {
        _throwing = throwing;
    }
    
    public void Act()
    {
        int x = 1;
        int y = 0;

        _throwing.Divide(x, y);
    }
}
