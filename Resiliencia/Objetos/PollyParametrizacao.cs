using System;
using System.Threading.Tasks;

namespace Resiliencia.Objetos
{
    public abstract class PollyParametrizacao<TReturn, TChild> where TChild : PollyParametrizacao<TReturn, TChild>
    {
        public TChild Child { get; protected set; }
        public Func<TReturn, bool> ResultCondition { get; protected set; }
        public Func<Exception, bool> ExpectionCondition { get; set; }
        public Action<TReturn, Exception, int> RetryHandler { get; protected set; }
        public Func<Task<TReturn>> TaskHandler { get; protected set; }
        public TReturn DefaultValue { get; protected set; }

        public TChild WithDefaultValue(TReturn value)
        {
            DefaultValue = value;
            return Child;
        }

        public TChild WithTaskHandler(Func<Task<TReturn>> handler)
        {
            TaskHandler = handler;
            return Child;
        }

        public TChild WithRetryHandler(Action<TReturn, Exception, int> handler)
        {
            RetryHandler = handler;
            return Child;
        }

        public TChild WithPollyExpectionCondition(Func<Exception, bool> condition)
        {
            ExpectionCondition = condition;
            return Child;
        }

        public TChild WithPollyResultCondition(Func<TReturn, bool> condition)
        {
            ResultCondition = condition;
            return Child;
        }
    }
}
