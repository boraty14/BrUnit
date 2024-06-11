using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class MonoFactory<T> : MonoBehaviour, IMonoFactory<T> where T : MonoBehaviour
    {
        private List<T> _monoUnits = new();
        
        public T CreateMono()
        {
            T mono = CreateMonoFromFactory();
            _monoUnits.Add(mono);
            return mono;
        }

        public void DeleteMono(T mono)
        {
            _monoUnits.Remove(mono);
            DeleteMonoFromFactory(mono);
        }
        
        protected abstract T CreateMonoFromFactory();
        protected abstract void DeleteMonoFromFactory(T mono);
    }
}