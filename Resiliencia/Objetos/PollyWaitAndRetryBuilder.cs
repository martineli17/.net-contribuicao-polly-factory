using System;
using System.Threading.Tasks;

namespace Resiliencia.Objetos
{
    public class PollyWaitAndRetryBuilder<TReturn> : PollyBaseBuilder<TReturn, PollyWaitAndRetryBuilder<TReturn>>
    {
        public PollyWaitAndRetryBuilder()
        {
            Child = this;
        }
        public PollyWaitAndRetryBuilder<TReturn> WithRetry(int retry)
        {
            Configurations.Retry = retry;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithMilliseconds(int milliseconds)
        {
            Configurations.Milliseconds = milliseconds;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithOnRetry(Action<TReturn, Exception, int> handler)
        {
            Configurations.OnRetry = handler;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithExpectionCondition(Func<Exception, bool> condition)
        {
            Configurations.ExpectionCondition = condition;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithResultCondition(Func<TReturn, bool> condition)
        {
            Configurations.ResultCondition = condition;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithOnCircuiteBreak(Func<Task> handler)
        {
            Configurations.OnCircuiteBreak = handler;
            return this;
        }

        public PollyWaitAndRetryBuilder<TReturn> WithOnCircuiteReset(Func<Task> handler)
        {
            Configurations.OnCircuiteReset = handler;
            return this;
        }
    }
}
