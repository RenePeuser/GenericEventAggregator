using System.Windows.Input;

namespace GenericEventAggregator.Contracts
{
    public interface IEventAggregator
    {
        void Subscribe<TClass>(TClass classToSubscribe) where TClass : class;

        void UnSubscribe<TClass>(TClass classToSubscribe) where TClass : class;

        void Publish<TEventArg>(TEventArg genericEvent) where TEventArg : class;
    }
}
