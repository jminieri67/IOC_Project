using System;

namespace IOC.Interfaces
{
    public interface IContainer
    {
        void Register<From, To>(LifecycleType lifecycleType = LifecycleType.Transient);
        void AddTransient<From, To>();
        void AddSingleton<From, To>();
        T Resolve<T>();
        object Create(Type type);

        bool Dispose(Type type);
    }
}
