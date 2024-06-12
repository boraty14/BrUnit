namespace Brecs
{
    public class DataUnitManager<T> : UnitManager<T> where T : struct, IDataUnit
    {
        protected override T CreateUnitPool()
        {
            return default;
        }
    }
}