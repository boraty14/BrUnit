using UnityEngine;

namespace Brecs
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
            monoUnit.Mono = _monoFactory.CreateMono();
            monoUnit.Position = Vector3.zero;
            monoUnit.Rotation = Quaternion.identity;
            monoUnit.Scale = Vector3.one;
            
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