using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnitManager<T> : MonoBehaviour where T : MonoUnit
    {
        private List<T> _monoUnits = new();
        
        private void OnEnable()
        {
            if (MonoUnits<T>.Manager != null)
            {
                Debug.LogError($"{typeof(T)} more than one factory");
            }
            MonoUnits<T>.Manager = this;
        }

        private void OnDisable()
        {
            MonoUnits<T>.Manager = null;
        }
        
        internal IReadOnlyCollection<T> GetMonoUnits() => _monoUnits;
        
        internal IEnumerable<(int index, T monoUnit)> EnumerateMonoUnits()
        {
            int index = 0;
            foreach (var monoUnit in _monoUnits)
            {
                yield return (index, monoUnit);
                index++;
            }
        }
        
        internal T CreateMonoUnit()
        {
            T monoUnit = CreateMonoUnitFromFactory();
            _monoUnits.Add(monoUnit);
            return monoUnit;
        }

        internal void DeleteMonoUnit(T monoUnit)
        {
            _monoUnits.Remove(monoUnit);
            DeleteMonoUnitFromFactory(monoUnit);
        }

        internal void DeleteIndices(List<int> indices)
        {
            indices.Sort();
            indices.Reverse();
            foreach (var index in indices)
            {
                if (index >= GetCount())
                {
                    continue;
                }
                DeleteMonoUnit(_monoUnits[index]);
            }
        }
        
        protected abstract T CreateMonoUnitFromFactory();
        protected abstract void DeleteMonoUnitFromFactory(T monoUnit);
        
        internal int GetCount() => _monoUnits.Count;
        internal bool IsEmpty() => GetCount() == 0;
        
        internal T GetSingleton()
        {
            int unitCount = _monoUnits.Count; 
            if (unitCount != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {unitCount}");
            }

            return _monoUnits[0];
        }
    }
}