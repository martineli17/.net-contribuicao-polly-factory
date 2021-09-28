using Polly;
using Polly.Wrap;
using Resiliencia.Objetos;

namespace Resiliencia.Setup
{
    public static class PollyFactoryRetryExtensions
    {
        public static AsyncPolicyWrap<TReturn> CreateRetryAsync<TReturn>(
            this PollyFactory pollyFactory, PollyConfigurations<TReturn> setup)
        {
            var policyRetry = Policy<TReturn>
                        .HandleResult(setup.ResultCondition)
                        .Or(setup.ExpectionCondition)
                        .RetryAsync(setup.Retry, (result, retry)
                        => setup.OnRetry(result.Result, result.Exception, retry));

            var policyCircuite = Policy.WrapAsync(pollyFactory.CreateCircuitBreaker(setup), policyRetry);

            return Policy.WrapAsync(pollyFactory.CreateFallback(setup), policyCircuite);
        }
    }
}
