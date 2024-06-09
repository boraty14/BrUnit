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

        private Dictionary<Type, List<object>> _reactives = new();

        protected abstract void InstallEngines();

        protected void AddStartEngine(IEngine engine)
        {
            _startEngines.Add(engine);
            AddReact(engine);
        }

        protected void AddUpdateEngine(IEngine engine)
        {
            _updateEngines.Add(engine);
            AddReact(engine);
        }

        protected void AddLateUpdateEngine(IEngine engine)
        {
            _lateUpdateEngines.Add(engine);
            AddReact(engine);
        }

        protected void AddFixedUpdateEngine(IEngine engine)
        {
            _fixedUpdateEngines.Add(engine);
            AddReact(engine);
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

        protected void AddReact<T>(T react)
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

        internal void React<T>(T reaction) where T : Reaction
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
    }
}