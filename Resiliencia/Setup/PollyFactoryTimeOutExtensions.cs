using Polly;
using Polly.Timeout;
using Polly.Wrap;
using Resiliencia.Objetos;

namespace Resiliencia.Setup
{
    public static class PollyFactoryTimeOutExtensions
    {
        public static AsyncPolicyWrap<TReturn> CreateTimeoutAsync<TReturn>(this PollyFactory pollyFactory, PollyConfigurations<TReturn> setup)
        {
            var policyTimeout = Policy.TimeoutAsync<TReturn>(setup.Milliseconds/1000, TimeoutStrategy.Optimistic);

            return Policy.WrapAsync(pollyFactory.CreateFallback(setup), policyTimeout);
        }
    }
}
