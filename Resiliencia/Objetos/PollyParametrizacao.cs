using System;
using System.Threading.Tasks;

namespace Resiliencia.Objetos
{
    public class PollyConfigurations<TReturn>
    {
        public Func<TReturn, bool> ResultCondition { get; internal set; }
        public Func<Exception, bool> ExpectionCondition { get; internal set; }
        public Action<Exception, TReturn> OnFallback { get; internal set; }
        public Action<TReturn, Exception, int> OnRetry { get; internal set; }
        public Func<Task> OnCircuiteBreak { get; internal set; }
        public Func<Task> OnCircuiteReset { get; internal set; }
        public Func<Task<TReturn>> OnHandler { get; internal set; }
        public TReturn DefaultValue { get; internal set; }
        public int Retry { get; internal set; }
        public int Milliseconds { get; internal set; }
        public int MillisecondsCircuitBreaker { get; internal set; }
        public int RetryCircuitBreaker { get; internal set; }
    }
}
