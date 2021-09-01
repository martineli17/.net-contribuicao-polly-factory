using Polly;
using Polly.Timeout;
using Resiliencia.Objetos;
using System.Threading.Tasks;

namespace Resiliencia.Setup
{
    public static class PollyFactoryTimeOutExtensions
    {
        public static async Task<TReturn> CreateTimeoutAsync<TReturn>(this IPollyFactory pollyFactory, PollyParametrizacaoTimeout<TReturn> setup)
        {
            var policy = Policy.TimeoutAsync<TReturn>(setup.Segundos, TimeoutStrategy.Optimistic);
            return await Policy
                    .WrapAsync(pollyFactory.CreateFallback(setup.DefaultValue), policy)
                    .ExecuteAsync(async () => await setup.TaskHandler());
        }
    }
}
