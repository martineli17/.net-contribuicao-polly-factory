using System;
using System.Threading.Tasks;

namespace Resiliencia.Objetos
{
    public class PollyRetryBuilder<TReturn> : PollyBaseBuilder<TReturn, PollyRetryBuilder<TReturn>>
    {
        public PollyRetryBuilder()
        {
            Child = this;
        }
        public PollyRetryBuilder<TReturn> WithRetry(int retry)
        {
            Configurations.Retry = retry;
            return this;
        }

        public PollyRetryBuilder<TReturn> WithOnRetry(Action<TReturn, Exception, int> handler)
        {
            Configurations.OnRetry = handler;
            return this;
        }

        public PollyRetryBuilder<TReturn> WithExpectionCondition(Func<Exception, bool> condition)
        {
            Configurations.ExpectionCondition = condition;
            return this;
        }

        public PollyRetryBuilder<TReturn> WithResultCondition(Func<TReturn, bool> condition)
        {
            Configurations.ResultCondition = condition;
            return this;
        }

        public PollyRetryBuilder<TReturn> WithOnCircuiteBreak(Func<Task> handler)
        {
            Configurations.OnCircuiteBreak = handler;
            return this;
        }

        public PollyRetryBuilder<TReturn> WithOnCircuiteReset(Func<Task> handler)
        {
            Configurations.OnCircuiteReset = handler;
            return this;
        }
    }
}
