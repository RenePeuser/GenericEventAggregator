namespace GenericEventAggregator.Contracts
{
    public interface IEvent<in TEventArg> : IEvent where TEventArg : class
    {
        void OnReceived(TEventArg eventArg);
    }

    public interface IEvent
    {
        
    }
}
