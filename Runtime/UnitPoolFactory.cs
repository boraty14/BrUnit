using System;
using UnityEngine;
using UnityEngine.Pool;

namespace BratyECS
{
    public abstract class UnitPoolFactory<T> : UnitFactory<T> where T : Unit
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

        protected override T CreateUnitFromFactory()
        {
            return Pool.Get();
        }

        protected override void DeleteUnitFromFactory(T unit)
        {
            Pool.Release(unit);
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
        protected virtual void GetSetup(T unit) => unit.gameObject.SetActive(true);
        protected virtual void ReleaseSetup(T unit) => unit.gameObject.SetActive(false);
        protected virtual void DestroySetup(T unit) => Destroy(unit);
        #endregion
    }
}