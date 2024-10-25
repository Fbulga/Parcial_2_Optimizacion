using System;
using System.Collections.Generic;

    public class EventService : IEventService 
    {
        private readonly Dictionary<string, Action> m_simpleEvents = new();
        private readonly Dictionary<Type, ICustomEventWrapper> m_complexEvents = new();
        private event Action<ICustomEventData> CallbackForAll;

        public void Initialize() {}

        public void AddListener(string p_guid, Action p_callback)
        {
            if (p_guid == null)
                return;

            if(!m_simpleEvents.ContainsKey(p_guid))
            {
                m_simpleEvents.Add(p_guid, p_callback);
                return;
            }
            
            m_simpleEvents[p_guid] += p_callback;
        }

        public void RemoveListener(string p_guid, Action p_callback)
        {
            if (!m_simpleEvents.ContainsKey(p_guid)) 
                return;
            
            m_simpleEvents[p_guid] -= p_callback;
        }

        public void DispatchEvent(string p_guid)
        {
            if (!m_simpleEvents.ContainsKey(p_guid))
                return;
            
            m_simpleEvents[p_guid]?.Invoke();
        }

        public void AddListener<T>(Action<T> p_callback) where T : ICustomEventData
        {
            var l_type = typeof(T);
            if(!m_complexEvents.ContainsKey(l_type))
            {
                var l_eventWrapper = new CustomEventWrapper<T>();
                l_eventWrapper.EventAction += p_callback;
                m_complexEvents.Add(l_type, l_eventWrapper);
                return;
            }

            if (m_complexEvents[l_type] is CustomEventWrapper<T> l_eventWrap)
                l_eventWrap.EventAction += p_callback;
        }

        public void RemoveListener<T>(Action<T> p_callback) where T : ICustomEventData
        {
            var l_type = typeof(T);
            if (!m_complexEvents.ContainsKey(l_type)) 
                return;
            
            if (m_complexEvents[l_type] is CustomEventWrapper<T> l_eventWrap)
                l_eventWrap.EventAction -= p_callback;
        }
        
        public void DispatchEvent<T>(T p_eventData) where T : ICustomEventData
        {
            CallbackForAll?.Invoke(p_eventData);
            
            var l_type = typeof(T);
            if (!m_complexEvents.ContainsKey(l_type))
                return;

            if (m_complexEvents[l_type] is CustomEventWrapper<T> l_eventWrap)
                l_eventWrap.Dispatch(p_eventData);
        }

        public void AddListenerForAll(Action<ICustomEventData> p_callback)
        {
            CallbackForAll += p_callback;
        }

        public void RemoveListenerForAll(Action<ICustomEventData> p_callback)
        {
            CallbackForAll -= p_callback;
        }
    }
