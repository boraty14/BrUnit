namespace BratyECS
{
    public interface IReact<in T> where T : struct, IReaction
    {
        void OnReact(T reaction);
    }
}