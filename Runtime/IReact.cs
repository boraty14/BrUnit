namespace BratyECS
{
    public interface IReact<in T> where T : Reaction
    {
        void OnReact(T reaction);
    }
}