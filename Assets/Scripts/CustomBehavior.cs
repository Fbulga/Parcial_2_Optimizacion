using Interfaces;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class CustomBehavior : MonoBehaviour , ICustomUpdated
    {
        public virtual void CustomStart() { }

        public virtual void CustomUpdate() { }

        public virtual void CustomFixedUpdate() { }

        public virtual void CustomLateUpdate() { }

        private void OnEnable()
        {
            CustomUpdateManager.Instance.OnStart += CustomStart;
            CustomUpdateManager.Instance.OnUpdate += CustomUpdate;
            CustomUpdateManager.Instance.OnFixedUpdate += CustomFixedUpdate;
            CustomUpdateManager.Instance.OnLateUpdate += CustomLateUpdate;
        }
        private void OnDisable()
        {
            CustomUpdateManager.Instance.OnStart -= CustomStart;
            CustomUpdateManager.Instance.OnUpdate -= CustomUpdate;
            CustomUpdateManager.Instance.OnFixedUpdate -= CustomFixedUpdate;
            CustomUpdateManager.Instance.OnLateUpdate -= CustomLateUpdate;
        }
    }
}