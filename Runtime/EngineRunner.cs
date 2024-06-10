using System;
using System.Collections.Generic;

namespace BratyECS
{
    public class EngineRunner : IDisposable
    {
        private readonly List<IEngine> _startEngines = new();
        private readonly List<IEngine> _updateEngines = new();
        private readonly List<IEngine> _lateUpdateEngines = new();
        private readonly List<IEngine> _fixedUpdateEngines = new();

        private Dictionary<Type, List<object>> _reactives = new();

        public EngineRunner()
        {
            Reactor.AddEngineRunner(this);
        }

        public void AddStartEngine(IEngine engine)
        {
            _startEngines.Add(engine);
            AddReact(engine);
        }

        public void AddUpdateEngine(IEngine engine)
        {
            _updateEngines.Add(engine);
            AddReact(engine);
        }

        public void AddLateUpdateEngine(IEngine engine)
        {
            _lateUpdateEngines.Add(engine);
            AddReact(engine);
        }

        public void AddFixedUpdateEngine(IEngine engine)
        {
            _fixedUpdateEngines.Add(engine);
            AddReact(engine);
        }

        public void Start()
        {
            TickEngines(_startEngines);
        }

        public void Update()
        {
            TickEngines(_updateEngines);
        }

        public void LateUpdate()
        {
            TickEngines(_lateUpdateEngines);
        }

        public void FixedUpdate()
        {
            TickEngines(_fixedUpdateEngines);
        }

        private void TickEngines(IReadOnlyCollection<IEngine> engines)
        {
            foreach (var engine in engines)
            {
                if (!engine.IsTickable())
                {
                    continue;
                }

                engine.Tick();
            }
        }

        public void AddReact<T>(T react) where T : class
        {
            var reactType = react.GetType();
            var interfaces = reactType.GetInterfaces();

            foreach (var interfaceType in interfaces)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IReact<>))
                {
                    var reactionType = interfaceType.GetGenericArguments()[0];

                    if (!_reactives.ContainsKey(reactionType))
                    {
                        _reactives[reactionType] = new();
                    }
                    _reactives[reactionType].Add(react);
                }
            }
        }

        internal void React<T>(T reaction) where T : struct, IReaction
        {
            var reactionType = typeof(T);
            
            if (!_reactives.TryGetValue(reactionType, out var reactiveEngines))
            {
                return;
            }

            foreach (var engine in reactiveEngines)
            {
                ((IReact<T>)engine).OnReact(reaction);
            }
        }

        public void Dispose()
        {
            Reactor.RemoveEngineRunner(this);
        }
    }
}