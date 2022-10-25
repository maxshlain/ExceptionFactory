using Castle.DynamicProxy;
using ExceptionFactoryCore;

namespace ExceptionFactoryTests;

public class DecoratorTest
{
    [Fact]
    public void aOriginal()
    {
        // ARRANGE
        var sut = new ThrowingClass();

        // ACT
        int x = 1;
        int y = 0;
        var res = sut.Divide(x, y);

        // ASSERT
        // should not throw
    }
    
    [Fact]
    public void bWithExplicitDecor()
    {
        // ARRANGE
        var sut = new LogicController();
        // ACT
        sut.Execute();

        // ASSERT
        // should not throw
    }
    
    [Fact]
    public void cWithInterception()
    {
        // ARRANGE
        var target = new ThrowingClass();
        var sut = Pro.Exceptions.InterceptAll(target);
        
        // ACT
        int x = 1;
        int y = 0;
        var res = sut.Divide(x, y);

        // ASSERT
        // should not throw
    }
}
