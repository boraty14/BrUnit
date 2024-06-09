using System;
using UnityEngine;

namespace BratyECS
{
    [Serializable]
    public class RefType<T>
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