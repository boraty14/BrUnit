using System.Collections.Generic;
using UnityEngine;

namespace Brecs
{
    public abstract class MonoFactory<T> : MonoBehaviour, IMonoFactory<T> where T : MonoBehaviour
    {
        private List<T> _monos = new();
        
        public T CreateMono()
        {
            T mono = CreateMonoFromFactory();
            _monos.Add(mono);
            return mono;
        }

        public void DeleteMono(T mono)
        {
            _monos.Remove(mono);
            DeleteMonoFromFactory(mono);
        }
        
        protected abstract T CreateMonoFromFactory();
        protected abstract void DeleteMonoFromFactory(T mono);
    }
}