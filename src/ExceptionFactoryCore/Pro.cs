using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;

namespace ExceptionFactoryCore;

public static class Pro
{
    public static readonly ProExceptions Exceptions = new();
}

public class ProExceptions
{
}

public static class ProExtensions
{
    private static ITranslationSettings TranslationSettings(ITranslationSettings input)
    {
        return input.ShowCapturedValues;
    }

    public static void ExecuteWithFacts(this ProExceptions _,
        Expression<Action> expressionAction,
        Func<dynamic> factsFactory)
    {
        try
        {
            expressionAction.Compile().Invoke();
        }
        catch (Exception exception)
        {
            var caller = expressionAction.ToReadableString(TranslationSettings);

            exception
                .On(caller)
                .WithFactsFactory(factsFactory)
                .Decorate();

            throw;
        }
    }
}
