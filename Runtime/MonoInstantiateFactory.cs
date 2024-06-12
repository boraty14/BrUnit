using UnityEngine;

namespace Brecs
{
    public abstract class MonoInstantiateFactory<T> : MonoFactory<T> where T : MonoBehaviour
    {
        [SerializeField] protected T Prefab;
        
        protected override T CreateMonoFromFactory()
        {
            return Instantiate(Prefab, transform);
        }

        protected override void DeleteMonoFromFactory(T mono)
        {
            Destroy(mono.gameObject);
        }
    }
}