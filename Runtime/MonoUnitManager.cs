using UnityEngine;

namespace BratyECS
{
    public class MonoUnitManager<TMonoUnit, TMonoBehaviour> : UnitManager<TMonoUnit>
        where TMonoUnit : MonoUnit<TMonoBehaviour> where TMonoBehaviour : MonoBehaviour
    {
        public override void RemoveUnit(TMonoUnit monoUnit)
        {
            monoUnit.Dispose();
            base.RemoveUnit(monoUnit);
        }
    }
}