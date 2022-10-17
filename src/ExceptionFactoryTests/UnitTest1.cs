using ExceptionFactoryCore;

namespace ExceptionFactoryTests;

public class UnitTest1
{
    [Fact]
    public void Test1()
    {
        // ARRANGE
        var sut = new Class1();
        // ACT
        var res = sut.Method(2, 3);

        // ASSERT
        Assert.Equal(res, 6);
    }
}
