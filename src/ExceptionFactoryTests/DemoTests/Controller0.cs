namespace ExceptionFactoryTests.DemoTests;

public class Controller0 : IController
{
    private readonly Actor _actor;

    public Controller0(Actor actor)
    {
        _actor = actor;
    }

    public void ExecuteBusinessLogic()
    {
        _actor.Act();
    }
}