using Polly;
using Polly.Fallback;
using System;

namespace Resiliencia.Setup
{
    public class PollyFactory : IPollyFactory
    {
        AsyncFallbackPolicy<TReturn> IPollyFactory.CreateFallback<TReturn>(TReturn defaultValue)
        {
            return Policy<TReturn>.Handle<Exception>().FallbackAsync(defaultValue);
        }
    }

    public interface IPollyFactory
    {
        internal AsyncFallbackPolicy<TReturn> CreateFallback<TReturn>(TReturn defaultValue);
    }
}
