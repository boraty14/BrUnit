namespace BratyECS
{
    public interface IEngine
    {
        bool IsTickable();
        void Tick();
    }
}