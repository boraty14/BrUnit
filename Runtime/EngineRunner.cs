using UnityEngine;

namespace Brecs
{
    public abstract class EngineRunner : MonoBehaviour
    {
        protected EngineContainer EngineContainer;

        protected abstract void InstallEngines();

        private void Awake()
        {
            EngineContainer = new EngineContainer();
            InstallEngines();
        }

        private void Start()
        {
            EngineContainer.Start();
        }

        private void Update()
        {
            EngineContainer.Update();
        }

        private void LateUpdate()
        {
            EngineContainer.LateUpdate();
        }

        private void FixedUpdate()
        {
            EngineContainer.FixedUpdate();
        }

        private void OnDestroy()
        {
            EngineContainer.Dispose();
        }
    }
}