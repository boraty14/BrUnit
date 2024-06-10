using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitSingletonManager<T> : MonoUnitManager<T> where T : MonoUnit
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

            CreateMonoUnit();
        }

        protected override T CreateMonoUnitFromManager()
        {
            if (_instance != null)
            {
                DeleteMonoUnit(_instance);
            }
            
            _instance = Instantiate(_prefab, transform);
            return _instance;
        }

        protected override void DeleteMonoUnitFromManager(T monoUnit)
        {
            if (_instance == null)
            {
                return;
            }
            Destroy(_instance.gameObject);
        }
    }
}