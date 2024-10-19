using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private Physics physics;
    // Start is called before the first frame update
    void Start()
    {
        physics = this.gameObject.GetComponent<Physics>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.ballOnBoard == false)
        {
            physics.ApplyGravity();
            physics.ApplyFriction();    
        }
        else
        {
            
        }
        
    }

    void CheckBorder()
    {
        
    }
}
