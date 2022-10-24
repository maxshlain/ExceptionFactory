using ExceptionFactoryCore;

namespace ExceptionFactoryTests;

public class LogicController
{
    private readonly ThrowingClass _throwingClass = new();

    public void Execute()
    {
        int x = 1;
        int y = 0;

        Pro.Exceptions.ExecuteWithFacts(
            () => _throwingClass.Divide(x, y),
            () => new { x, y }
        );
    }
}
