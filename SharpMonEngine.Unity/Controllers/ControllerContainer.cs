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

            if (!Controllers.TryAdd(controllerType, controller))
            {
                throw new InvalidOperationException(
                    $"The controller type '{controllerType.Name}' has already been registered.");
            }
        }

        public static TType Get<TType>()
        {
            Type controllerType = typeof(TType);

            if (!Controllers.TryGetValue(controllerType, out object controller) && controller != null)
            {
                ThrowNotRegisteredException(controllerType);
            }

            return (TType)controller!;
        }

        public static void UnRegister<TType>()
        {
            Type controllerType = typeof(TType);
            if (!Controllers.Remove(controllerType, out object controller))
            {
                ThrowNotRegisteredException(controllerType);
            }
        }

        private static void ThrowAlreadyRegisteredException(Type controllerType)
        {
            throw new InvalidOperationException(
                $"The controller type '{controllerType.Name}' has already been registered.");
        }

        private static void ThrowNotRegisteredException(Type controllerType)
        {
            throw new InvalidOperationException(
                $"The controller type '{controllerType.Name}' has not been registered.");
        }
    }
}