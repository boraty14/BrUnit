using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitInstantiateManager<T> : MonoUnitManager<T> where T : MonoUnit
    {
        [SerializeField] private T _prefab;
        
        protected override T CreateMonoUnitFromManager()
        {
            return Instantiate(_prefab, transform);
        }

        protected override void DeleteMonoUnitFromManager(T monoUnit)
        {
            Destroy(monoUnit.gameObject);
        }
    }
}