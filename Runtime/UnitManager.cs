using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class UnitManager<T>
    {
        private readonly List<T> _units = new();

        public void AddUnitSingleton(T monoUnit)
        {
            if (_units.Count != 0)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {_units.Count}");
                _units.Clear();
            }
            AddUnit(monoUnit);
        }
        public void AddUnit(T monoUnit) => _units.Add(monoUnit);
        public virtual void RemoveUnit(T monoUnit) => _units.Remove(monoUnit);

        public void RemoveIndex(int index)
        {
            var unit = _units[index];
            RemoveUnit(unit);
        }

        public void RemoveIndices(List<int> indices)
        {
            indices.Sort();
            indices.Reverse();
            foreach (var index in indices)
            {
                if (index >= GetCount())
                {
                    continue;
                }
                RemoveUnit(_units[index]);
            }
        }
        
        public void ClearUnits() => _units.Clear();
        public IReadOnlyCollection<T> GetUnits() => _units;
        public IEnumerable<(int index, T unit)> EnumerateUnits()
        {
            int index = 0;
            foreach (var unit in _units)
            {
                yield return (index, unit);
                index++;
            }
        }

        public T GetSingleton()
        {
            int unit = _units.Count; 
            if (unit != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {unit}");
            }

            return _units[0];
        }
        public int GetCount() => _units.Count;
        public bool IsEmpty() => GetCount() == 0;
    }
}