using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitInstantiateManager<T> : MonoUnitManager<T> where T : MonoUnit
    {
        [SerializeField] private T _prefab;
        
        protected override T CreateMonoUnitFromFactory()
        {
            return Instantiate(_prefab, transform);
        }

        protected override void DeleteMonoUnitFromFactory(T monoUnit)
        {
            Destroy(monoUnit);
        }
    }
}