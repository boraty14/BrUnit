using System;
using UnityEngine;

namespace Brecs
{
    public abstract class MonoUnit<T> : IUnit where T : MonoBehaviour
    {
        internal T Mono;
        public abstract void Reset();

        protected void MonoAction(Action<T> monoAction)
        {
            if (monoAction == null || Mono == null)
            {
                return;
            }

            monoAction(Mono);
        }

        #region MonoUnitTransform

        private Vector3 _position;
        public Vector3 Position
        {
            get => _position;
            set
            {
                _position = value;
                MonoAction(SetPosition);
            }
        }

        private void SetPosition(T mono)
        {
            mono.transform.position = _position;
        }

        private Quaternion _rotation;
        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                MonoAction(SetRotation);
            }
        }
        
        private void SetRotation(T mono)
        {
            mono.transform.rotation = _rotation;
        }

        private Vector3 _scale;
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                MonoAction(SetScale);
            }
        }

        private void SetScale(T mono)
        {
            mono.transform.localScale = _scale;
        }

        #endregion
    }
}