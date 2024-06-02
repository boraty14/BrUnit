using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public static class UnitFactoryManager<T> where T : Unit
        {
        internal static UnitFactory<T> Factory;
        
        public static T CreateUnit() => Factory.CreateUnit();
        public static void DeleteUnit(T unit) => Factory.DeleteUnit(unit);
        public static IReadOnlyCollection<T> GetInstances() => Factory.GetInstances();
        public static IEnumerable<(int index, T instance)> EnumerateInstances() => Factory.EnumerateInstances();
        public static T GetSingleton() => Factory.GetSingleton();
        public static int GetCount() => Factory.GetCount();
        public static bool IsEmpty() => GetCount() == 0;
    }
}