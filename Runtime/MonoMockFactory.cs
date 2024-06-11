using UnityEngine;

namespace BrUnit
{
    public class MonoMockFactory<T> : IMonoFactory<T> where T : MonoBehaviour
    {
        public T CreateMono()
        {
            return null;
        }

        public void DeleteMono(T mono)
        {
        }
    }
}