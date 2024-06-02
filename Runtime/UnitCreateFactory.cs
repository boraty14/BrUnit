using UnityEngine;

namespace BratyECS
{
    public abstract class UnitCreateFactory<T> : UnitFactory<T> where T : Unit
    {
        [SerializeField] private T _prefab;
        
        protected override T CreateUnitFromFactory()
        {
            return Instantiate(_prefab, transform);
        }

        protected override void DeleteUnitFromFactory(T unit)
        {
            Destroy(unit);
        }
    }
}