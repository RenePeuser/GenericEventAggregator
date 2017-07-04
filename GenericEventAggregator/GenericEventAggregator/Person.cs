using System;
using GenericEventAggregator.Contracts;

namespace GenericEventAggregator
{
    public class Person : BindingBaseOptimized
    {
        public Person(string name, string familyName, DateTime birthday)
        {
            Name = name;
            FamilyName = familyName;
            Birthday = birthday;
        }

        public string Name
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public string FamilyName
        {
            get { return Get<string>(); }
            set { Set(value); }
        }

        public DateTime Birthday
        {
            get { return Get<DateTime>(); }
            set { Set(value); }
        }
    }
}
