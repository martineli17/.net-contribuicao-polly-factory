namespace Resiliencia.Objetos
{
    public class PollyParametrizacaoRetryAndWait<TReturn> : PollyParametrizacao<TReturn, PollyParametrizacaoRetryAndWait<TReturn>>
    {
        public int Retry { get; private set; }
        public int Milliseconds { get; private set; }
        public PollyParametrizacaoRetryAndWait()
        {
            Child = this;
        }
    }
}
