using UnityEngine;

namespace BratyECS
{
    public class MonoUnit<T> where T : MonoBehaviour
    {
        internal readonly T Mono;

        protected MonoUnit(T mono)
        {
            Mono = mono;
        }
    }
}