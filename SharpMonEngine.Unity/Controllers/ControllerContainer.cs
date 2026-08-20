using System;
using System.Collections.Generic;

namespace SharpMonEngine.Unity.Controllers
{
    public static class ControllerContainer
    {
        private static readonly Dictionary<Type, object> Controllers = new Dictionary<Type, object>();

        public static void Register<TType, TImpl>(TImpl controller) where TImpl : notnull, TType
        {
            Type controllerType = typeof(TType);

            if (Controllers.ContainsKey(controllerType))
            {
                throw new InvalidOperationException(
                    $"The controller type '{controllerType.Name}' has already been registered.");
            }

            Controllers.Add(controllerType, controller);
        }

        public static TType Get<TType>()
        {
            Type controllerType = typeof(TType);

            if (!Controllers.TryGetValue(controllerType, out object controller))
            {
                throw new InvalidOperationException(
                    $"The controller type '{controllerType.Name}' has not been registered.");
            }

            return (TType)controller;
        }

        public static void UnRegister<TType>()
        {
            Type controllerType = typeof(TType);
            if (!Controllers.TryGetValue(controllerType, out object controller))
            {
                return;
            }

            Controllers.Remove(controllerType);
        }
    }
}