using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class UnitFactory<T> : MonoBehaviour where T : Unit
    {
        private List<T> _units = new();
        
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
        
        internal IReadOnlyCollection<T> GetUnits() => _units;
        
        internal IEnumerable<(int index, T unit)> EnumerateUnits()
        {
            int index = 0;
            foreach (var unit in _units)
            {
                yield return (index, unit);
                index++;
            }
        }
        
        internal T CreateUnit()
        {
            T unit = CreateUnitFromFactory();
            _units.Add(unit);
            return unit;
        }

        internal void DeleteUnit(T unit)
        {
            _units.Remove(unit);
            DeleteUnitFromFactory(unit);
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
                DeleteUnit(_units[index]);
            }
        }
        
        protected abstract T CreateUnitFromFactory();
        protected abstract void DeleteUnitFromFactory(T unit);
        
        internal int GetCount() => _units.Count;
        internal bool IsEmpty() => GetCount() == 0;
        
        internal T GetSingleton()
        {
            int unitCount = _units.Count; 
            if (unitCount != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {unitCount}");
            }

            return _units[0];
        }
    }
}