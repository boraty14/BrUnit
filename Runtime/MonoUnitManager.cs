using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public class MonoUnitManager<TMonoUnit, TMono> : UnitManager<TMonoUnit>
        where TMonoUnit : MonoUnit<TMono>, new() where TMono : MonoBehaviour
    {
        private readonly IMonoFactory<TMono> _monoFactory;
        private readonly Stack<TMonoUnit> _unitPool;

        public MonoUnitManager(IMonoFactory<TMono> monoFactory)
        {
            _monoFactory = monoFactory;
            _unitPool = new();
        }

        public override TMonoUnit AddUnit()
        {
            TMonoUnit monoUnit = GetUnitFromPool();
            monoUnit.Reset();
            
            TMono mono = _monoFactory.CreateMono();
            monoUnit.Mono = mono;
            
            Units.Add(monoUnit);
            return monoUnit;
        }

        public override void RemoveUnit(TMonoUnit unit)
        {
            if (unit.Mono != null)
            {
                _monoFactory.DeleteMono(unit.Mono);
            }

            Units.Remove(unit);
            ReturnUnitToPool(unit);
        }

        private TMonoUnit GetUnitFromPool()
        {
            if (_unitPool.Count == 0)
            {
                return new TMonoUnit();
            }

            return _unitPool.Pop();
        }

        private void ReturnUnitToPool(TMonoUnit unit)
        {
            _unitPool.Push(unit);
        }
    }
}