using UnityEngine;

namespace BratyECS
{
    public class MonoUnit<T> where T : MonoBehaviour
    {
        internal readonly T MonoBehaviour;

        protected MonoUnit(T monoBehaviour)
        {
            MonoBehaviour = monoBehaviour;
        }
    }
}