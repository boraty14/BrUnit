using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnit<T> : MonoBehaviour where T : MonoUnit<T>
    {
        private bool _isRegistered = false;
        
        protected virtual void OnEnable()
        {
            if (_isRegistered)
            {
                return;
            }
            
            MonoUnitManager<T>.Register(this as T);
            _isRegistered = true;
        }

        protected virtual void OnDisable()
        {
            if (!_isRegistered)
            {
                return;
            }
            
            MonoUnitManager<T>.Unregister(this as T);
            _isRegistered = false;
        }
    }
}