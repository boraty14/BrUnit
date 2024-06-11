using System;
using UnityEngine;

namespace BratyECS
{
    public class MonoUnitManager<TMonoUnit, TMonoBehaviour> : UnitManager<TMonoUnit>
        where TMonoUnit : MonoUnit<TMonoBehaviour> where TMonoBehaviour : MonoBehaviour
    {
        private readonly IMonoUnitFactory<TMonoBehaviour> _monoUnitFactory;
        
        public MonoUnitManager(IMonoUnitFactory<TMonoBehaviour> monoUnitFactory)
        {
            _monoUnitFactory = monoUnitFactory;
        }

        public override TMonoUnit AddUnit()
        {
            TMonoBehaviour monoBehaviour = _monoUnitFactory.CreateMonoUnit();
            object[] args = {monoBehaviour};
            TMonoUnit monoUnit = Activator.CreateInstance(typeof(TMonoUnit), args) as TMonoUnit;
            Units.Add(monoUnit);
            return monoUnit;
        }

        public override void RemoveUnit(TMonoUnit unit)
        {
            Units.Remove(unit);
            if (unit.MonoBehaviour == null)
            {
                return;
            }
            _monoUnitFactory.DeleteMonoUnit(unit.MonoBehaviour);
        }
    }
}