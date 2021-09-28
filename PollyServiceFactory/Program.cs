using Resiliencia.Objetos;
using Resiliencia.Setup;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PollyServiceFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            var parametrosPolly = CreatePollyParametrizacao();
            var policy = PollyFactory.Create().CreateRetryAsync(parametrosPolly.Get());

            while (true)
            {
                var result = policy.ExecuteAsync(async () => await Service.Execute()).GetAwaiter().GetResult();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(result);
                Console.ForegroundColor = ConsoleColor.White;
                Task.Delay(2000).Wait();
            }
        }

        private static PollyRetryBuilder<string> CreatePollyParametrizacao()
        {
            return new PollyRetryBuilder<string>()
                .WithDefaultValue("Sem resultado")
                .WithExpectionCondition(x => x.Message == "Operação não está válida")
                .WithResultCondition(x => new[] { "3", "4", "5", "6" }.Contains(x))
                .WithOnRetry((result, exception, retry) =>
                {
                    if (exception is not null)
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
                .WithRetry(2);
        }
    }
}
