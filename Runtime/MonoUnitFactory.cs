using System;
using UnityEngine;
using UnityEngine.Pool;

namespace BratyECS
{
    public abstract class MonoUnitFactory<T> : MonoBehaviour where T : MonoUnit<T> 
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

        protected virtual void Awake()
        {
            InitPool(_prefab);
        }

        private void OnEnable()
        {
            MonoUnitFactoryManager<T>.Instance = this;
        }

        private void OnDisable()
        {
            MonoUnitFactoryManager<T>.Instance = null;
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
        protected virtual T CreateSetup() => Instantiate(_prefab);
        protected virtual void GetSetup(T obj) => obj.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(T obj) => obj.gameObject.SetActive(false);
        protected virtual void DestroySetup(T obj) => Destroy(obj);
        #endregion

        #region Getters
        public T Get() => Pool.Get();
        public void Release(T obj) => Pool.Release(obj);
        #endregion
    }
}