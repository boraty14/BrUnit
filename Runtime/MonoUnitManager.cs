using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public class MonoUnitManager<TMonoUnit, TMono> : UnitManager<TMonoUnit>
        where TMonoUnit : MonoUnit<TMono>, new() where TMono : MonoBehaviour
    {
        private readonly IMonoFactory<TMono> _monoFactory;

        public MonoUnitManager(IMonoFactory<TMono> monoFactory)
        {
            _monoFactory = monoFactory;
        }

        public override TMonoUnit AddUnit()
        {
            TMonoUnit monoUnit = base.AddUnit();
            TMono mono = _monoFactory.CreateMono();
            monoUnit.Mono = mono;
            
            return monoUnit;
        }

        public override void RemoveUnit(TMonoUnit unit)
        {
            base.RemoveUnit(unit);   
            if (unit.Mono != null)
            {
                _monoFactory.DeleteMono(unit.Mono);
            }
        }
    }
}