namespace BratyECS
{
    public class DataUnitManager<T> : UnitManager<T> where T : struct, IDataUnit
    {
        public override T AddUnit()
        {
            T unit = default;
            Units.Add(unit);
            return unit;
        }

        public override void RemoveUnit(T unit)
        {
            Units.Remove(unit);
        }
    }
}