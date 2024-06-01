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
        
        protected abstract void InstallEngines();

        protected void AddStartEngine(IEngine engine) => _startEngines.Add(engine);
        protected void AddUpdateEngine(IEngine engine) => _updateEngines.Add(engine);
        protected void AddLateUpdateEngine(IEngine engine) => _lateUpdateEngines.Add(engine);
        protected void AddFixedUpdateEngine(IEngine engine) => _fixedUpdateEngines.Add(engine);

        private void Awake()
        {
            InstallEngines();
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
    }
}