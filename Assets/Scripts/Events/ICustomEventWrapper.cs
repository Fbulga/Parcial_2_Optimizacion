
    public interface ICustomEventWrapper {}
    
    public interface ICustomEventWrapper<T> : ICustomEventWrapper where T : ICustomEventData {}
