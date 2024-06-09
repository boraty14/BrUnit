using System;
using UnityEngine;

namespace BratyECS
{
    [Serializable]
    public class RefType<T> where T : class
    {
        [SerializeField] private T _ref;

        public T Ref
        {
            get => _ref;
            set => _ref = value;
        }

        public T ReadRef => _ref;
    }
}