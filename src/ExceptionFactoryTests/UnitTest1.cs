using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using ExceptionFactoryCore;
using Microsoft.Diagnostics.Runtime;

namespace ExceptionFactoryTests;

public class LogicController
{
    private readonly ThrowingClass _throwingClass = new ThrowingClass();

    public void Execute()
    {
        int x = 1;
        int y = 0;

        try
        {
            _throwingClass.Divide(x, y);

            //WrapperClass.Wrap(() => _throwingClass.Divide(x,y));
        }
        catch (Exception exception)
        {
            var id = Process.GetCurrentProcess().Id;
            Attacher.Attach(id);
            exception
                .On("Divide")
                .WithArgument(x)
                .WithArgument(y)
                .Decorate();

            throw;
        }
    }
}

public class Attacher
{
    public static void Attach(int processId)
    {
        Console.WriteLine("here");
        try
        {
            using var dataTarget = DataTarget.AttachToProcess(processId, false);
            Console.WriteLine("attached");

            var clr = dataTarget.ClrVersions.Single().CreateRuntime();
            var thread = clr.Threads.First(t => t.ManagedThreadId == Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("found");
            
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}

public class WrapperClass
{
    public static T Wrap<T>(Expression<Func<T>> func)
    {
        var type = func.Body.GetType();
        var fieldInfo = type.GetProperty("Arguments");
        var propValue = (ReadOnlyCollection<Expression>) fieldInfo.GetValue(func.Body);

        var first = propValue[0];
        var firstType = first.GetType();
        var firstFieldInfo = firstType.GetProperty("Expression");
        var expressionValue = (ConstantExpression) firstFieldInfo.GetValue(first);

        var xxx = (Expression) expressionValue;
        
        var second = expressionValue.GetType();
        var secondType = second.GetType();

        Console.WriteLine(first);
        var compiled = func.Compile();
        return    compiled.Invoke();
    }
}