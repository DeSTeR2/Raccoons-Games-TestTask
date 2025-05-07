using System;
using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Event
{
    [CreateAssetMenu(fileName = "All events", menuName = "Event/AllEvents")]
    public class AllEvents : ScriptableObject
    {
        public List<Events> allEvents;

        private readonly Dictionary<Type, Events> _eventsMap = new();

        public TEvents Find<TEvents>() where TEvents : Events
        {
            if (_eventsMap.ContainsKey(typeof(TEvents))) return (TEvents)_eventsMap[typeof(TEvents)];

            foreach (var events in allEvents)
                if (events.GetType() == typeof(TEvents))
                {
                    _eventsMap.Add(typeof(TEvents), events);
                    return (TEvents)events;
                }

            throw new Exception($"Did not found event typeof: {typeof(TEvents)}");
        }
    }
}