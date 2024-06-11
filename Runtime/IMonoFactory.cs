using UnityEngine;

namespace BrUnit
{
    public interface IMonoFactory<T> where T : MonoBehaviour
    {
        T CreateMono();
        void DeleteMono(T mono);
    }
}