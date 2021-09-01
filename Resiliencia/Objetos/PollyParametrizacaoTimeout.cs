namespace Resiliencia.Objetos
{
    public class PollyParametrizacaoTimeout<TReturn> : PollyParametrizacao<TReturn, PollyParametrizacaoTimeout<TReturn>>
    {
        public int Segundos { get; set; }
        public PollyParametrizacaoTimeout()
        {
            Child = this;
        }
    }
}
