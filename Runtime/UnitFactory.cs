using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class UnitFactory<T> : MonoBehaviour where T : Unit
    {
        private List<T> _instances = new();
        
        private void OnEnable()
        {
            if (UnitFactoryManager<T>.Factory != null)
            {
                Debug.LogError($"{typeof(T)} more than one factory");
            }
            UnitFactoryManager<T>.Factory = this;
        }

        private void OnDisable()
        {
            UnitFactoryManager<T>.Factory = null;
        }
        
        internal IReadOnlyCollection<T> GetInstances() => _instances;
        
        internal IEnumerable<(int index, T instance)> EnumerateInstances()
        {
            int index = 0;
            foreach (var instance in _instances)
            {
                yield return (index, instance);
                index++;
            }
        }
        
        internal T CreateUnit()
        {
            T unit = CreateUnitFromFactory();
            _instances.Add(unit);
            return unit;
        }

        internal void DeleteUnit(T unit)
        {
            _instances.Remove(unit);
            DeleteUnitFromFactory(unit);
        }
        
        protected abstract T CreateUnitFromFactory();
        protected abstract void DeleteUnitFromFactory(T unit);
        
        internal int GetCount() => _instances.Count;
        internal bool IsEmpty() => GetCount() == 0;
        
        internal T GetSingleton()
        {
            int instanceCount = _instances.Count; 
            if (instanceCount != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, instance count {instanceCount}");
            }

            return _instances[0];
        }
    }
}