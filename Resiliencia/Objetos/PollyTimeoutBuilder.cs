namespace Resiliencia.Objetos
{
    public class PollyTimeoutBuilder<TReturn> : PollyBaseBuilder<TReturn, PollyTimeoutBuilder<TReturn>>
    {
        public PollyTimeoutBuilder()
        {
            Child = this;
        }
        public PollyTimeoutBuilder<TReturn> WithMilliseconds(int milliseconds)
        {
            Configurations.Milliseconds = milliseconds;
            return this;
        }
    }
}
