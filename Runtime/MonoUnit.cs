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
                MonoAction(mono => mono.transform.position = value);
            }
        }

        private Quaternion _rotation;
        public Quaternion Rotation
        {
            get => _rotation;
            set
            {
                _rotation = value;
                MonoAction(mono => mono.transform.rotation = value);
            }
        }

        private Vector3 _scale;
        public Vector3 Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                MonoAction(mono => mono.transform.localScale = value);
            }
        }

        #endregion
    }
}