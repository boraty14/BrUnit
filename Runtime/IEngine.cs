namespace BratyECS
{
    public interface IEngine
    {
        void Tick();
        bool IsTickable();
    }
}