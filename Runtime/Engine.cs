namespace BrUnit
{
    public abstract class Engine
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
    }
}