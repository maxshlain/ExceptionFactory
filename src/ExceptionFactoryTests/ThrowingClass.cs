namespace ExceptionFactoryTests;

public class ThrowingClass
{
    public virtual double Divide(int x, int y)
    {
        // if(y == 0)
        // {
        //     throw new Exception("Divide by 0 not allowed");
        // }

        // if (y == 0) return 0;

        return x / y;
    }
}
