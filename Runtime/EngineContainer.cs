using UnityEngine;

namespace BratyECS
{
    public abstract class EngineContainer : MonoBehaviour
    {
        private EngineRunner _engineRunner;

        protected abstract void InstallEngines();
        
        protected void AddStartEngine(IEngine engine) => _engineRunner.AddStartEngine(engine);
        protected void AddUpdateEngine(IEngine engine) => _engineRunner.AddUpdateEngine(engine);
        protected void AddLateUpdateEngine(IEngine engine) => _engineRunner.AddLateUpdateEngine(engine);
        protected void AddFixedUpdateEngine(IEngine engine) => _engineRunner.AddFixedUpdateEngine(engine);
        protected void AddReact<T>(T reaction) where T: class => _engineRunner.AddReact(reaction);
        
        private void Awake()
        {
            _engineRunner = new EngineRunner();
            InstallEngines();
        }
        
        private void Start()
        {
            _engineRunner.Start();
        }
        
        private void Update()
        {
            _engineRunner.Update();
        }
        
        private void LateUpdate()
        {
            _engineRunner.LateUpdate();
        }
        
        private void FixedUpdate()
        {
            _engineRunner.FixedUpdate();
        }
        
        private void OnDestroy()
        {
            _engineRunner.Dispose();
        }
    }
}