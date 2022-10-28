using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

[assembly: System.Resources.NeutralResourcesLanguage("en-us")]

namespace ExceptionFactoryCodeGen
{
    [Generator]
    public partial class DecoratedGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            IncrementalValuesProvider<ClassDeclarationSyntax?> classDeclarations = context.SyntaxProvider
                    .CreateSyntaxProvider(
                    Predicate,
                    static (ctx, _) => GetSemanticTargetForGeneration(ctx))
                ;

            IncrementalValueProvider<(Compilation, ImmutableArray<ClassDeclarationSyntax?>)> compilationAndClasses =
                context.CompilationProvider.Combine(classDeclarations.Collect());

            context.RegisterSourceOutput(compilationAndClasses, static (spc, source) => Execute(source.Item1, source.Item2, spc));
        }

        internal static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
        {
            return null;
        }

        private bool Predicate(SyntaxNode arg1, CancellationToken arg2)
        {
            return true;
        }

        private static void Execute(
            Compilation compilation, 
            ImmutableArray<ClassDeclarationSyntax?> classes, 
            SourceProductionContext context)
        {
            string source = @"

namespace FunNs
{
    public class Say
    {
        public string Hello()
        {
            return ""Hello"";
        }
    }
}
";
            
            context.AddSource("Fun.Say.g.cs", SourceText.From(source, Encoding.UTF8));
        }
    }
}

// namespace Fun
// {
//     public class Say
//     {
//         public string Hello()
//         {
//             return "Hello";
//         }
//     }
// }
