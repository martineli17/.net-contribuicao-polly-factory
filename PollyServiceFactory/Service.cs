using System;
using System.Threading.Tasks;

namespace PollyServiceFactory
{
    public class Service
    {
        public static Task<string> Execute()
        {
            var isValid = new Random().Next(1, 3) == 2;
            if (isValid)
                return Task.FromResult(new Random().Next(3, 10).ToString());
            else
                throw new ApplicationException("Operação não está válida");
        }
    }
}
