using System;
using System.Threading.Tasks;

namespace Resiliencia.Objetos
{
    public abstract class PollyBaseBuilder<TReturn, TChild>
    {
        protected PollyConfigurations<TReturn> Configurations;
        protected TChild Child;
        public PollyBaseBuilder()
        {
            Configurations = new PollyConfigurations<TReturn>();
            Configurations.RetryCircuitBreaker = 3;
            Configurations.MillisecondsCircuitBreaker = 2000;
            Configurations.OnCircuiteBreak = () =>
            {
                Console.WriteLine("ON CIRCUITE BREAK");
                return Task.CompletedTask;
            };
            Configurations.OnCircuiteReset = () =>
            {
                Console.WriteLine("ON CIRCUITE RESET");
                return Task.CompletedTask;
            };
        }
        public TChild WithDefaultValue(TReturn value)
        {
            Configurations.DefaultValue = value;
            return Child;
        }

        public TChild WithOnFallback(Action<Exception, TReturn> handler)
        {
            Configurations.OnFallback = handler;
            return Child;
        }

        public PollyConfigurations<TReturn> Get() => Configurations;
    }
}
