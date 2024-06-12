using System;

namespace Brecs
{
    public abstract class Engine : IDisposable
    {
        public void Tick()
        {
            if (!IsTickable())
            {
                return;
            }
            TickEngine();
        }
        
        protected abstract void TickEngine();
        protected abstract bool IsTickable();
        public virtual void Dispose()
        {
            
        }
    }
}