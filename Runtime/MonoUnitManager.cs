using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public static class MonoUnitManager<T> where T : MonoUnit<T>
    {
        private static readonly List<T> Instances = new List<T>();
        public static IReadOnlyCollection<T> GetInstances() => Instances;
        public static int GetCount() => Instances.Count;
        public static bool IsEmpty() => GetCount() == 0;

        public static T GetSingleton()
        {
            int instanceCount = Instances.Count; 
            if (instanceCount != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, instance count {instanceCount}");
            }

            return Instances[0];
        }
        
        internal static void Register(T instance)
        {
            Instances.Add(instance);
        }

        internal static void Unregister(T instance)
        {
            Instances.Remove(instance);
        }
    }
}