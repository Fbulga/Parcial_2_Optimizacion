using Managers;
using UnityEngine;

public abstract class CustomBehaviour : MonoBehaviour
{
    protected virtual void CustomStart() { }

    protected virtual void CustomUpdate() { }

    protected virtual void CustomFixedUpdate() { }

    protected virtual void CustomLateUpdate() { }

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