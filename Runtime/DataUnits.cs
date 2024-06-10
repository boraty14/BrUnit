using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public static class DataUnits<T> where T : struct, IDataUnit
    {
        private static readonly List<T> _dataUnits = new List<T>();

        public static void AddDataUnitSingleton(T dataUnit)
        {
            if (_dataUnits.Count != 0)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {_dataUnits.Count}");
                _dataUnits.Clear();
            }
            AddDataUnit(dataUnit);
        }
        public static void AddDataUnit(T dataUnit) => _dataUnits.Add(dataUnit);
        public static void RemoveDataUnit(T dataUnit) => _dataUnits.Remove(dataUnit);

        public static void RemoveIndices(List<int> indices)
        {
            indices.Sort();
            indices.Reverse();
            foreach (var index in indices)
            {
                if (index >= GetCount())
                {
                    continue;
                }
                RemoveDataUnit(_dataUnits[index]);
            }
        }
        
        public static void ClearDataUnits() => _dataUnits.Clear();
        public static IReadOnlyCollection<T> GetDataUnits() => _dataUnits;
        public static IEnumerable<(int index, T dataUnit)> EnumerateDataUnits()
        {
            int index = 0;
            foreach (var dataUnit in _dataUnits)
            {
                yield return (index, dataUnit);
                index++;
            }
        }

        public static T GetSingleton()
        {
            
            int dataUnit = _dataUnits.Count; 
            if (dataUnit != 1)
            {
                Debug.LogError($"{typeof(T)} is not singleton, unit count {dataUnit}");
            }

            return _dataUnits[0];
        }
        public static int GetCount() => _dataUnits.Count;
        public static bool IsEmpty() => GetCount() == 0;
        
    }
}