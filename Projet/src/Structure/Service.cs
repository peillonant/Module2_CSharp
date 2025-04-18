public static class Services
{
    static Dictionary<Type , object> _services = []; 

    public static void Register<T>(T service)
    {
        if(_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of type {typeof(T)} already registered");

        if (service == null)
            throw new ArgumentNullException("service");

        _services.Add(typeof(T), service);
    }

    public static T Get<T>()
    {
        if(!_services.ContainsKey(typeof(T)))
            throw new InvalidOperationException($"Service of type {typeof(T)} is not registered");
        
        return (T) _services[typeof(T)];
    }

}