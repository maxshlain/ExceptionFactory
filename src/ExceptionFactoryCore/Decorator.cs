using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace ExceptionFactoryCore;

public static class ExceptionExtensions
{
    public static void SetMessage(this Exception exception, string message)
    {
        ArgumentNullException.ThrowIfNull(exception);

        var type = exception.GetType();
        var flags = BindingFlags.Instance | BindingFlags.NonPublic;
        var fieldInfo = type.GetField("_message", flags);
        fieldInfo?.SetValue(exception, message);
    }
}

public record DecoratorMetadata<T>(T OriginalException, string[] Metadata) where T: Exception;

public static class Decorator
{
    public static DecoratorMetadata<T> On<T>(
        this T exception,
        string called
    ) where T: Exception
    {
        var calledMetadata = $"Called: {called}";
        return new DecoratorMetadata<T>(exception, new []{calledMetadata});
    }
    
    public static void Decorate<T>(
        this DecoratorMetadata<T> decoratorMetadata
    )
        where T: Exception

    {
        var sb = new StringBuilder(decoratorMetadata.OriginalException.Message);
        sb.AppendLine();
        
        foreach (var m in decoratorMetadata.Metadata)
        {
            sb.AppendLine(m);
        }
        
        decoratorMetadata.OriginalException.SetMessage(sb.ToString());
    }

    public static DecoratorMetadata<T> WithArgument<T>(
        this DecoratorMetadata<T> decoratorMetadata, 
        object argument,
        [CallerArgumentExpression("argument")] 
        string? argumentName = null
        )
            where T: Exception

    {
        var addition = $" With Argument: {argumentName}:{argument}";
        var newMetadata = decoratorMetadata.Metadata.Concat(new []{addition}).ToArray();
        return decoratorMetadata with { Metadata = newMetadata };
    }
}
