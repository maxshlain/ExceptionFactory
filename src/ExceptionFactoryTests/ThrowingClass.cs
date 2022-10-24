namespace ExceptionFactoryTests;

public class ThrowingClass
{
    public double Divide(int x, int y)
    {
        // if(y == 0)
        // {
        //     throw new Exception("Divide by 0 not allowed");
        // }

        return x / y;
    }
}