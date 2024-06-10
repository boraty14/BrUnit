using System.Collections.Generic;

namespace BratyECS
{
    public static class MonoUnits<T> where T : MonoUnit
    {
        internal static MonoUnitManager<T> Manager;

        public static T CreateMonoUnit() => Manager.CreateMonoUnit();
        public static void DeleteMonoUnit(T monoUnit) => Manager.DeleteMonoUnit(monoUnit);
        public static void DeleteIndices(List<int> indices) => Manager.DeleteIndices(indices);
        public static void ClearMonoUnits() => Manager.ClearMonoUnits();
        public static IReadOnlyCollection<T> GetMonoUnits() => Manager.GetMonoUnits();
        public static IEnumerable<(int index, T monoUnit)> EnumerateMonoUnits() => Manager.EnumerateMonoUnits();
        public static T GetSingleton() => Manager.GetSingleton();
        public static int GetCount() => Manager.GetCount();
        public static bool IsEmpty() => GetCount() == 0;
    }
}