using Polly;
using Resiliencia.Objetos;
using System;
using System.Threading.Tasks;

namespace Resiliencia.Setup
{
    public static class PollyFactoryRetryAndWaitExtensions
    {
        public static async Task<TReturn> CreateWaitAndRetryAsync<TReturn>(
            this IPollyFactory pollyFactory, PollyParametrizacaoRetryAndWait<TReturn> setup)
        {
            var policy = Policy<TReturn>
                        .HandleResult(setup.ResultCondition)
                        .Or(setup.ExpectionCondition)
                        .WaitAndRetryAsync(setup.Retry, sleep => TimeSpan.FromMilliseconds(setup.Milliseconds),
                         (result, time, retry, context) => setup.RetryHandler(result.Result, result.Exception, retry));
            return await Policy
                    .WrapAsync(pollyFactory.CreateFallback(setup.DefaultValue), policy)
                    .ExecuteAsync(async () => await setup.TaskHandler());
        }
    }
}
