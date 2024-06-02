using System.Collections.Generic;

namespace BratyECS
{
    public static class UnitFactoryManager<T> where T : Unit
        {
        internal static UnitFactory<T> Factory;
        
        public static T CreateUnit() => Factory.CreateUnit();
        public static void DeleteUnit(T unit) => Factory.DeleteUnit(unit);
        public static void DeleteIndices(List<int> indices) => Factory.DeleteIndices(indices);
        public static IReadOnlyCollection<T> GetUnits() => Factory.GetUnits();
        public static IEnumerable<(int index, T unit)> EnumerateUnits() => Factory.EnumerateUnits();
        public static T GetSingleton() => Factory.GetSingleton();
        public static int GetCount() => Factory.GetCount();
        public static bool IsEmpty() => GetCount() == 0;
    }
}