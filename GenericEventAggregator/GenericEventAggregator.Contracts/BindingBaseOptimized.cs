using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Extensions;

namespace GenericEventAggregator.Contracts
{
    public abstract class BindingBaseOptimized : BindingBase
    {
        private readonly Dictionary<string, object> _propertyFieldDictionary;

        protected BindingBaseOptimized()
        {
            _propertyFieldDictionary = new Dictionary<string, object>();
        }

        public T Get<T>([CallerMemberName] string propertyName = "")
        {
            if (!_propertyFieldDictionary.ContainsKey(propertyName))
            {
                return default(T);
            }

            return _propertyFieldDictionary[propertyName].Cast<T>();
        }

        public bool Set<T>(T newValue, [CallerMemberName] string propertyName = "")
        {
            var currentValue = Get<T>(propertyName);
            if (newValue.EqualityEquals(currentValue))
            {
                return false;
            }
            _propertyFieldDictionary[propertyName] = newValue;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
