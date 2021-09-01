using Polly;
using Resiliencia.Objetos;
using System.Threading.Tasks;

namespace Resiliencia.Setup
{
    public static class PollyFactoryRetryExtensions
    {
        public static async Task<TReturn> CreateRetryWithFallbackAsync<TReturn>(
            this IPollyFactory pollyFactory, PollyParametrizacaoRetry<TReturn> setup)
        {
            var policy = Policy<TReturn>
                        .HandleResult(setup.ResultCondition)
                        .Or(setup.ExpectionCondition)
                        .RetryAsync(setup.Retry, (result, retry)
                        => setup.RetryHandler(result.Result, result.Exception, retry));

            return await Policy
                    .WrapAsync(pollyFactory.CreateFallback(setup.DefaultValue), policy)
                    .ExecuteAsync(async () => await setup.TaskHandler());
        }
    }
}
