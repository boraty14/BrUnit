using System.Collections.Generic;
using UnityEngine;

namespace Brecs
{
    public abstract class UnitManager<T> where T : IUnit, new()
    {
        private readonly List<T> _units = new();
        private readonly Stack<T> _unitPool = new();

        public virtual T AddUnit()
        {
            T unit = GetUnitFromPool();
            unit.Reset();
            _units.Add(unit);
            return unit;
        }

        public virtual void RemoveUnit(T unit)
        {
            _units.Remove(unit);
            ReturnUnitToPool(unit);
        }

        private T GetUnitFromPool()
        {
            if (_unitPool.Count == 0)
            {
                return new T();
            }

            return _unitPool.Pop();
        }

        private void ReturnUnitToPool(T unit)
        {
            _unitPool.Push(unit);
        }

        public T AddSingle()
        {
            int unitCount = GetCount();
            if (unitCount != 0)
            {
                Debug.LogError($"{typeof(T)} is not single, unit count {unitCount}");
                ClearUnits();
            }

            return AddUnit();
        }
        
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

        public void ClearUnits()
        {
            foreach (var unit in _units)
            {
                RemoveUnit(unit);
            }
        }

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

        public T GetSingle()
        {
            int unitCount = GetCount();
            if (unitCount != 1)
            {
                Debug.LogError($"{typeof(T)} is not single, unit count {unitCount}");
            }

            return _units[0];
        }

        public int GetCount() => _units.Count;
        public bool IsEmpty() => GetCount() == 0;
    }
}