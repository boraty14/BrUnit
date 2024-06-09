using System;
using UnityEngine;

namespace BratyECS
{
    [Serializable]
    public struct ValType<T> where T : struct
    {
        [SerializeField] private T _value;

        public T Value
        {
            get => _value;
            set => _value = value;
        }

        public T ReadValue => _value;
    }
}