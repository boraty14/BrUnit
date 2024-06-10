using System;
using UnityEngine;

namespace BratyECS
{
    public abstract class MonoUnit<T> : IDisposable where T : MonoBehaviour
    {
        protected readonly T MonoBehaviour;
        private readonly IMonoUnitFactory<T> _monoUnitFactory;

        protected MonoUnit(IMonoUnitFactory<T> monoUnitFactory)
        {
            MonoBehaviour = monoUnitFactory.CreateMonoUnit();
        }

        public void Dispose()
        {
            _monoUnitFactory.DeleteMonoUnit(MonoBehaviour);
        }
    }
}