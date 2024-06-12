using UnityEngine;

namespace Brecs
{
    public abstract class MonoUnit<T> : IUnit where T : MonoBehaviour
    {
        protected internal T Mono { get; internal set; }
        public abstract void Reset();
    }
}