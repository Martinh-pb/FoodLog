using System;
using Unity;
using Unity.Lifetime;

namespace FoodLog
{
    public static class Injector
    {
        private static readonly UnityContainer unityContainer = new UnityContainer();

        public static void RegisterType<I, T>() where T : I
        {
            unityContainer.RegisterType<I, T>(new ContainerControlledLifetimeManager());
        }

        public static T Resolve<T>()
        {
            return unityContainer.Resolve<T>();
        }
    }
}
