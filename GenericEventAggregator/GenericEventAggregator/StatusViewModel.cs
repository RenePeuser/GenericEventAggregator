using GenericEventAggregator.Contracts;

namespace GenericEventAggregator
{
    public class StatusViewModel : BindingBaseOptimized, IEvent<Person>
    {
        private readonly IEventAggregator _eventAggregator;

        public StatusViewModel(IEventAggregator EventAggregator)
        {
            _eventAggregator = EventAggregator;
            _eventAggregator.Subscribe(this);
        }

        public Person Person
        {
            get { return Get<Person>(); }
            set { Set(value); }
        }

        public void OnReceived(Person eventArg)
        {
            Person = eventArg;
        }

        public void Cleanup()
        {
            _eventAggregator.UnSubscribe(this);
        }
    }
}
