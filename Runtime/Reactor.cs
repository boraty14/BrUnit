using System.Collections.Generic;

namespace BratyECS
{
    internal static class Reactor
    {
        private static readonly List<EngineRunner> _engineRunners = new();
        internal static IReadOnlyCollection<EngineRunner> EngineRunners => _engineRunners;

        internal static void AddEngineRunner(EngineRunner engineRunner) => _engineRunners.Add(engineRunner);
        internal static void RemoveEngineRunner(EngineRunner engineRunner) => _engineRunners.Remove(engineRunner);
    }
    
    public static class Reactor<T> where T : struct, IReaction
    {
        public static void Emit(T reaction = default)
        {
            foreach (var engineRunner in Reactor.EngineRunners)
            {
                engineRunner.React(reaction);
            }
        }
    }
}