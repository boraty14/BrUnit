namespace Brecs
{
    public class ModelUnitManager<T> : UnitManager<T> where T : ModelUnit, new()
    {
        protected override T CreateUnitPool()
        {
            return new T();
        }
    }
}