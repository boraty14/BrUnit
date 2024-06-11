namespace BrUnit
{
    public class DataUnitManager<T> : UnitManager<T> where T : class, IDataUnit, new()
    {
    }
}