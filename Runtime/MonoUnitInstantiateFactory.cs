using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitInstantiateFactory<T> : MonoUnitFactory<T> where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        
        protected override T CreateMonoUnitFromFactory()
        {
            return Instantiate(_prefab, transform);
        }

        protected override void DeleteMonoUnitFromFactory(T monoUnit)
        {
            Destroy(monoUnit.gameObject);
        }
    }
}