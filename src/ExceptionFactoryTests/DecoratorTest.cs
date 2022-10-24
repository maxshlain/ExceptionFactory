namespace ExceptionFactoryTests;

public class DecoratorTest
{
    [Fact]
    public void ShouldDecorate()
    {
        // ARRANGE
        var sut = new LogicController();
        // ACT
        sut.Execute();

        // ASSERT
        // should not throw
    }
}