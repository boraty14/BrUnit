using System;
using System.Collections.Generic;
using UnityEngine;

namespace BratyECS
{
    public abstract class EngineRunner : MonoBehaviour
    {
        private readonly List<IEngine> _startEngines = new();
        private readonly List<IEngine> _updateEngines = new();
        private readonly List<IEngine> _lateUpdateEngines = new();
        private readonly List<IEngine> _fixedUpdateEngines = new();

        private Dictionary<Type, List<object>> _reactiveEngines = new();

        protected abstract void InstallEngines();

        protected void AddStartEngine(IEngine engine)
        {
            _startEngines.Add(engine);
            AddReactions(engine);
        }

        protected void AddUpdateEngine(IEngine engine)
        {
            _updateEngines.Add(engine);
            AddReactions(engine);
        }

        protected void AddLateUpdateEngine(IEngine engine)
        {
            _lateUpdateEngines.Add(engine);
            AddReactions(engine);
        }

        protected void AddFixedUpdateEngine(IEngine engine)
        {
            _fixedUpdateEngines.Add(engine);
            AddReactions(engine);
        }

        private void Awake()
        {
            Reactor.AddEngineRunner(this);
            InstallEngines();
        }

        private void OnDestroy()
        {
            Reactor.RemoveEngineRunner(this);
        }

        private void Start()
        {
            TickEngines(_startEngines);
        }

        private void Update()
        {
            TickEngines(_updateEngines);
        }

        private void LateUpdate()
        {
            TickEngines(_lateUpdateEngines);
        }

        private void FixedUpdate()
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

        private void AddReactions(IEngine engine)
        {
            var engineType = engine.GetType();
            var interfaces = engineType.GetInterfaces();

            foreach (var interfaceType in interfaces)
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IReact<>))
                {
                    var reactionType = interfaceType.GetGenericArguments()[0];

                    if (!_reactiveEngines.ContainsKey(reactionType))
                    {
                        _reactiveEngines[reactionType] = new();
                    }
                    _reactiveEngines[reactionType].Add(engine);
                }
            }
        }

        internal void React<T>(T reaction) where T : Reaction
        {
            var reactionType = typeof(T);
            
            if (!_reactiveEngines.TryGetValue(reactionType, out var reactiveEngines))
            {
                return;
            }

            foreach (var engine in reactiveEngines)
            {
                ((IReact<T>)engine).OnReact(reaction);
            }
        }
    }
}