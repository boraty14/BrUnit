using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitFactory<T> : MonoBehaviour, IMonoUnitFactory<T> where T : MonoBehaviour
    {
        private List<T> _monoUnits = new();
        
        public T CreateMonoUnit()
        {
            T monoUnit = CreateMonoUnitFromFactory();
            _monoUnits.Add(monoUnit);
            return monoUnit;
        }

        public void DeleteMonoUnit(T monoUnit)
        {
            _monoUnits.Remove(monoUnit);
            DeleteMonoUnitFromFactory(monoUnit);
        }
        
        protected abstract T CreateMonoUnitFromFactory();
        protected abstract void DeleteMonoUnitFromFactory(T monoUnit);
    }
}