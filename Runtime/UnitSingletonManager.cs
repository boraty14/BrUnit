using UnityEngine;

namespace BratyECS
{
    public abstract class UnitSingletonManager<T> : UnitManager<T> where T : Unit
    {
        [SerializeField] private T _prefab;
        [SerializeField] private bool _isLazy; 
        private T _instance;

        private void Awake()
        {
            if (_isLazy)
            {
                return;
            }

            CreateUnit();
        }

        protected override T CreateUnitFromFactory()
        {
            if (_instance != null)
            {
                DeleteUnit(_instance);
            }
            
            _instance = Instantiate(_prefab, transform);
            return _instance;
        }

        protected override void DeleteUnitFromFactory(T unit)
        {
            if (_instance == null)
            {
                return;
            }
            Destroy(_instance);
        }
    }
}