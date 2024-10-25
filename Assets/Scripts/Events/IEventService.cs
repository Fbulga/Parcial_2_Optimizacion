using System;


    public interface IEventService 
    {
        void AddListener(string p_guid, Action p_callback);
        void RemoveListener(string p_guid, Action p_callback);
        void DispatchEvent(string p_guid);

        void AddListener<T>(Action<T> p_callback) where T : ICustomEventData;
        void RemoveListener<T>(Action<T> p_callback) where T : ICustomEventData;
        void DispatchEvent<T>(T p_eventData) where T : ICustomEventData;
        void AddListenerForAll(Action<ICustomEventData> p_callback);
        void RemoveListenerForAll(Action<ICustomEventData> p_callback);
    }
