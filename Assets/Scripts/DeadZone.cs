using Managers;
using UnityEngine;

public class DeadZone : CustomBehaviour
{
    public bool IsDeadly = true;
    private float timer = 0f;
    [SerializeField] private float deadlyTime;

    public void RunDeadlyTimer()
    { 
        IsDeadly = false;
        timer = 0f;
    }

    protected override void CustomStart()
    {
        GameManager.Instance.SetDeadZone(this);
    }

    protected override void CustomUpdate()
    {
        if (!IsDeadly)
        {
            timer += Time.deltaTime;
            if (timer >= deadlyTime) IsDeadly = true;
        }
    }
}