using System;
using UnityEngine;
using UnityEngine.Pool;

namespace BratyECS
{
    public abstract class MonoPoolFactory<T> : MonoFactory<T> where T : MonoBehaviour
    {
        [SerializeField] private T _prefab;
        private ObjectPool<T> _pool;

        private ObjectPool<T> Pool {
            get {
                if (_pool == null) throw new InvalidOperationException("You need to call InitPool before using it.");
                return _pool;
            }
            set => _pool = value;
        }

        protected override T CreateMonoFromFactory()
        {
            return Pool.Get();
        }

        protected override void DeleteMonoFromFactory(T mono)
        {
            Pool.Release(mono);
        }

        protected virtual void Awake()
        {
            InitPool(_prefab);
        }

        protected void InitPool(T prefab, int initial = 10, int max = 100, bool collectionChecks = false) {
            _prefab = prefab;
            Pool = new ObjectPool<T>(
                CreateSetup,
                GetSetup,
                ReleaseSetup,
                DestroySetup,
                collectionChecks,
                initial,
                max);
        }

        #region Overrides
        protected virtual T CreateSetup() => Instantiate(_prefab, transform);
        protected virtual void GetSetup(T mono) => mono.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(T mono) => mono.gameObject.SetActive(false);
        protected virtual void DestroySetup(T mono) => Destroy(mono.gameObject);
        #endregion
    }
}