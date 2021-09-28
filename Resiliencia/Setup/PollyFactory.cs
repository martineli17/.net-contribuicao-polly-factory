using Polly;
using Polly.CircuitBreaker;
using Polly.Fallback;
using Resiliencia.Objetos;
using System;
using System.Threading.Tasks;

namespace Resiliencia.Setup
{
    public class PollyFactory
    {
        public static PollyFactory Create() => new PollyFactory();

        internal AsyncCircuitBreakerPolicy<TReturn> CreateCircuitBreaker<TReturn>(PollyConfigurations<TReturn> setup)
        {
            return Policy<TReturn>.HandleResult(result => setup.ResultCondition(result))
                                 .Or<Exception>()
                                 .CircuitBreakerAsync(setup.RetryCircuitBreaker, TimeSpan.FromMilliseconds(setup.MillisecondsCircuitBreaker),
                                 onBreak: (result, TimeSpan) => setup.OnCircuiteBreak(),
                                 onReset: () => setup.OnCircuiteReset());
        }

        internal AsyncFallbackPolicy<TReturn> CreateFallback<TReturn>(PollyConfigurations<TReturn> setup)
        {
            return Policy<TReturn>.HandleResult(result => setup.ResultCondition(result))
                                  .Or<Exception>()
                                  .FallbackAsync(setup.DefaultValue, result =>
                                  {
                                      if (setup.OnFallback != null)
                                          setup.OnFallback(result.Exception, result.Result);
                                      return Task.CompletedTask;
                                  });
        }
    }
}
