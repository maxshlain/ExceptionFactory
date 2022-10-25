using Castle.DynamicProxy;

namespace ExceptionFactoryCore;

[Serializable]
public class ExceptionInterceptor : IInterceptor
{
    public void Intercept(IInvocation invocation)
    {
        try
        {
            invocation.Proceed();
        }
        catch(Exception exception)
        {
            exception
                .On(invocation.Method.ToString() ?? "")
                .WithFactsFactory(() => invocation.Arguments ?? Array.Empty<object>())
                .Decorate();

            throw;
        }
    }
}
