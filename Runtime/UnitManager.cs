using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class UnitManager<T>
    {
        protected readonly List<T> Units = new();

        public abstract T AddUnit();

        public abstract void RemoveUnit(T unit);

        public void RemoveIndex(int index)
        {
            var unit = Units[index];
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
                RemoveUnit(Units[index]);
            }
        }
        
        public void ClearUnits() => Units.Clear();
        public IReadOnlyCollection<T> GetUnits() => Units;
        public IEnumerable<(int index, T unit)> EnumerateUnits()
        {
            int index = 0;
            foreach (var unit in Units)
            {
                yield return (index, unit);
                index++;
            }
        }

        public T GetSingleton()
        {
            int unit = Units.Count; 
            if (unit != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {unit}");
            }

            return Units[0];
        }
        public int GetCount() => Units.Count;
        public bool IsEmpty() => GetCount() == 0;
    }
}