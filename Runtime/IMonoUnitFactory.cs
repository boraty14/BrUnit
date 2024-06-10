using UnityEngine;

namespace BratyECS
{
    public interface IMonoUnitFactory<T> where T : MonoBehaviour
    {
        T CreateMonoUnit();
        void DeleteMonoUnit(T monoUnit);
    }
}