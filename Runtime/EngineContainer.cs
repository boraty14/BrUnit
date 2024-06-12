using System;
using System.Collections.Generic;

namespace Brecs
{
    public class EngineContainer : IDisposable
    {
        private readonly List<Engine> _startEngines = new List<Engine>();
        private readonly List<Engine> _updateEngines = new List<Engine>();
        private readonly List<Engine> _lateUpdateEngines = new List<Engine>();
        private readonly List<Engine> _fixedUpdateEngines = new List<Engine>();
        
        public void AddStartEngine(Engine engine)
        {
            _startEngines.Add(engine);
        }
        
        public void AddUpdateEngine(Engine engine)
        {
            _updateEngines.Add(engine);
        }
        
        public void AddLateUpdateEngine(Engine engine)
        {
            _lateUpdateEngines.Add(engine);
        }
        
        public void AddFixedUpdateEngine(Engine engine)
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
        
        private void TickEngines(IReadOnlyCollection<Engine> engines)
        {
            foreach (var engine in engines)
            {
                engine.Tick();
            }
        }

        public void Dispose()
        {
            DisposeEngines(_startEngines);    
            DisposeEngines(_updateEngines);
            DisposeEngines(_lateUpdateEngines);
            DisposeEngines(_fixedUpdateEngines);
        }
        
        private void DisposeEngines(IReadOnlyCollection<Engine> engines)
        {
            foreach (var engine in engines)
            {
                engine.Dispose();
            }
        }
    }
}