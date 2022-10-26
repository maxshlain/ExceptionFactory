namespace ExceptionFactoryTests.DemoTests;

public class Controller1 : IController
{
    private readonly DivideByZeroActorWithProExceptionWrapper _actor;

    public Controller1(DivideByZeroActorWithProExceptionWrapper actor)
    {
        _actor = actor;
    }

    public void ExecuteBusinessLogic()
    {
        _actor.Act();
    }
}
