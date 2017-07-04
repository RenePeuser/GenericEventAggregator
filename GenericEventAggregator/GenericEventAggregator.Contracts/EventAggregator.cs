using System;
using System.Collections.Generic;
using System.Linq;
using Extensions;

namespace GenericEventAggregator.Contracts
{
    public class EventAggregator : IEventAggregator
    {
        private List<object> attachedObjects = new List<object>();

        public void Subscribe<TClass>(TClass classToSubscribe) where TClass : class
        {
            if (!attachedObjects.Contains(classToSubscribe))
            {
                attachedObjects.Add(classToSubscribe);
            }
        }

        public void UnSubscribe<TClass>(TClass classToSubscribe) where TClass : class
        {
            attachedObjects.Remove(classToSubscribe);
        }

        public void Publish<TEventArg>(TEventArg genericEvent) where TEventArg : class
        {
            var subscribers = attachedObjects.OfType<IEvent<TEventArg>>();
            subscribers.ForEach(subscriber => subscriber.OnReceived(genericEvent));
        }
    }

    public class EventAggregatorWithCache : IEventAggregator
    {
        private List<object> attachedObjects = new List<object>();
        private Dictionary<Type, object> eventCache = new Dictionary<Type, object>();

        public void Subscribe<TClass>(TClass classToSubscribe) where TClass : class
        {
            if (!attachedObjects.Contains(classToSubscribe))
            {
                attachedObjects.Add(classToSubscribe);

                var expectedInterfaces = classToSubscribe.GetType().GetInterfaces().Where(item => typeof(IEvent).IsAssignableFrom(item));
                var interfacesForCachedInfo = expectedInterfaces.Where(i => i.GenericTypeArguments.Any(type => eventCache.ContainsKey(type))).SelectMany(i => i.GetGenericArguments());

                foreach (var type in interfacesForCachedInfo)
                {
                    var method = GetType().GetMethod(nameof(Publish));
                    var genericMethod = method.MakeGenericMethod(type);
                    genericMethod.Invoke(this, new[] { eventCache[type] });
                }

            }
        }

        public void UnSubscribe<TClass>(TClass classToSubscribe) where TClass : class
        {
            attachedObjects.Remove(classToSubscribe);
        }

        public void Publish<TEventArg>(TEventArg genericEvent) where TEventArg : class
        {
            eventCache[typeof(TEventArg)] = genericEvent;

            var subscribers = attachedObjects.OfType<IEvent<TEventArg>>();
            subscribers.ForEach(subscriber => subscriber.OnReceived(genericEvent));
        }
    }
}
