using System.Linq.Expressions;
using AgileObjects.ReadableExpressions;
using Castle.DynamicProxy;

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

    public static T InterceptAll<T>(
        this ProExceptions _,
        T target
    ) where T : class
    {
        var proxyGen = new ProxyGenerator();
        var interceptor = new ExceptionInterceptor();
        T? sut = proxyGen.CreateClassProxyWithTarget(target, interceptor);

        return sut ?? throw new Exception();
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
