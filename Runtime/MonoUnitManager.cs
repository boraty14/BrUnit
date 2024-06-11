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
            TMono mono = _monoFactory.CreateMono();
            TMonoUnit monoUnit = new TMonoUnit
            {
                Mono = mono
            };
            Units.Add(monoUnit);
            return monoUnit;
        }

        public override void RemoveUnit(TMonoUnit unit)
        {
            Units.Remove(unit);
            if (unit.Mono == null)
            {
                return;
            }
            _monoFactory.DeleteMono(unit.Mono);
        }
    }
}