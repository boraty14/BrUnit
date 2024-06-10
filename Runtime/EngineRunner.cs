using System;
using System.Collections.Generic;

namespace BratyECS
{
    public class EngineRunner
    {
        private readonly List<IEngine> _startEngines = new();
        private readonly List<IEngine> _updateEngines = new();
        private readonly List<IEngine> _lateUpdateEngines = new();
        private readonly List<IEngine> _fixedUpdateEngines = new();

        private Dictionary<Type, List<object>> _reactives = new();

        public void AddStartEngine(IEngine engine)
        {
            _startEngines.Add(engine);
        }

        public void AddUpdateEngine(IEngine engine)
        {
            _updateEngines.Add(engine);
        }

        public void AddLateUpdateEngine(IEngine engine)
        {
            _lateUpdateEngines.Add(engine);
        }

        public void AddFixedUpdateEngine(IEngine engine)
        {
            _fixedUpdateEngines.Add(engine);
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
    }
}