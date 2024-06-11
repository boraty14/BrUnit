using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnit<T> where T : MonoBehaviour
    {
        protected internal T Mono { get; internal set; }
    }
}