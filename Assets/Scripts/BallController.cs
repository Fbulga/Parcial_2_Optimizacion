using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IPhysics, ISphere
{
    private CustomPhysicsNuestro customPhysicsNuestro;
    // Start is called before the first frame update
    void Start()
    {
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckBorder()
    {
        
    }
}
