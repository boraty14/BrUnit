using System.Collections.Generic;

namespace BratyECS
{
    public static class Units<T> where T : Unit
    {
        internal static UnitManager<T> Manager;

        public static T CreateUnit() => Manager.CreateUnit();
        public static void DeleteUnit(T unit) => Manager.DeleteUnit(unit);
        public static void DeleteIndices(List<int> indices) => Manager.DeleteIndices(indices);
        public static IReadOnlyCollection<T> GetUnits() => Manager.GetUnits();
        public static IEnumerable<(int index, T unit)> EnumerateUnits() => Manager.EnumerateUnits();
        public static T GetSingleton() => Manager.GetSingleton();
        public static int GetCount() => Manager.GetCount();
        public static bool IsEmpty() => GetCount() == 0;
    }
}