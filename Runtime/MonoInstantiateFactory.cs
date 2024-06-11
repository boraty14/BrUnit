using UnityEngine;

namespace BratyECS
{
    public abstract class MonoInstantiateFactory<T> : MonoFactory<T> where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        
        protected override T CreateMonoFromFactory()
        {
            return Instantiate(_prefab, transform);
        }

        protected override void DeleteMonoFromFactory(T mono)
        {
            Destroy(mono.gameObject);
        }
    }
}