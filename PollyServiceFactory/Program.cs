using Resiliencia.Objetos;
using Resiliencia.Setup;
using System;
using System.Linq;

namespace PollyServiceFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var polly = new PollyFactory();
            PollyParametrizacaoRetry<string> parametrosPolly = CreatePollyParametrizacao();
            var result = polly.CreateRetryWithFallbackAsync(parametrosPolly).GetAwaiter().GetResult();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(result);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static PollyParametrizacaoRetry<string> CreatePollyParametrizacao()
        {
            return new PollyParametrizacaoRetry<string>()
                .WithDefaultValue("Sem resultado")
                .WithTaskHandler(() => Service.Execute())
                .WithPollyExpectionCondition(x => x.Message == "Operação não está válida")
                .WithPollyResultCondition(x => new[] { "3","4","5", "6" }.Contains(x))
                .WithRetryHandler((result, exception, retry) =>
                {
                    if(exception is not null)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"TENTATIVA {retry} | EXCEPTION {exception.GetType().Name}");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"TENTATIVA {retry} | RESULT {result}");
                    }
                })
                .WithRetry(3);
        }
    }
}
