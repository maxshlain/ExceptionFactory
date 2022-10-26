namespace ExceptionFactoryTests.DemoTests;

public class ThrowingClass
{
    public virtual double Divide(int x, int y)
    {
        // ReSharper disable once PossibleLossOfFraction
        return x / y;
    }
}
