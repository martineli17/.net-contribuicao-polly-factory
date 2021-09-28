using Polly;
using Polly.Wrap;
using Resiliencia.Objetos;
using System;

namespace Resiliencia.Setup
{
    public static class PollyFactoryRetryAndWaitExtensions
    {
        public static AsyncPolicyWrap<TReturn> CreateWaitAndRetryAsync<TReturn>(
           this PollyFactory pollyFactory, PollyConfigurations<TReturn> setup)
        {
            var policyRetry = Policy<TReturn>
                        .HandleResult(setup.ResultCondition)
                        .Or(setup.ExpectionCondition)
                        .WaitAndRetryAsync(setup.Retry, sleep => TimeSpan.FromMilliseconds(setup.Milliseconds),
                         (result, time, retry, context) => setup.OnRetry(result.Result, result.Exception, retry));

            var policyCircuite = Policy.WrapAsync(pollyFactory.CreateCircuitBreaker(setup), policyRetry);

            return Policy.WrapAsync(pollyFactory.CreateFallback(setup), policyCircuite);
        }
    }
}
