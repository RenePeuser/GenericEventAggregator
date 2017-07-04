using System;
using System.Collections.ObjectModel;
using GenericEventAggregator.Contracts;

namespace GenericEventAggregator
{
    public class MainWindowViewModel : BindingBaseOptimized
    {
        private readonly IEventAggregator _eventAggregator;

        public MainWindowViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            Persons = new ObservableCollection<Person>()
            {
                new Person("Max", "Mustermann", new DateTime(1999, 1, 21)),
                new Person("Peter", "Mustermann", new DateTime(2000, 5,10)),
                new Person("Horst", "Mustermann", new DateTime(1990, 7, 3)),
                new Person("Georg", "Mustermann", new DateTime(1980, 4, 7)),
            };
        }

        public ObservableCollection<Person> Persons
        {
            get { return Get<ObservableCollection<Person>>(); }
            set { Set(value); }
        }

        public Person SelectedPerson
        {
            get
            {
                return Get<Person>();
            }

            set
            {
                if (Set(value))
                {
                    _eventAggregator.Publish(value);
                }
            }
        }
    }
}
