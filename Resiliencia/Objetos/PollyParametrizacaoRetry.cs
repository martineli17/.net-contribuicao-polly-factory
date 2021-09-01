namespace Resiliencia.Objetos
{
    public class PollyParametrizacaoRetry<TReturn> : PollyParametrizacao<TReturn, PollyParametrizacaoRetry<TReturn>>
    {
        public int Retry { get; protected set; }
        public PollyParametrizacaoRetry()
        {
            Child = this;
        }
        public PollyParametrizacaoRetry<TReturn> WithRetry(int retry)
        {
            Retry = retry;
            return this;
        }
    }
}
