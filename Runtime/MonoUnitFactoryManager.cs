namespace BratyECS
{
    public static class MonoUnitFactoryManager<T> where T : MonoUnit<T>
    {
        internal static MonoUnitFactory<T> Instance;

        public static T Get()
        {
            return Instance.Get();
        }

        public static void Release(T unit)
        {
            Instance.Release(unit);
        }
    }
}