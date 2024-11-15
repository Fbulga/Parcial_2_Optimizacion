using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IPhysics, ISphere
{
    private CustomPhysicsNuestro customPhysicsNuestro;

    [SerializeField] private float radius;
    private Collider[] colliders = new Collider[5];

    private SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        if (!GameManager.Instance.ballOnBoard)
        {
            CheckBorder();
        }
    }

    void CheckBorder()
    {
        
        foreach (var collider in colliders)
        {
            if (collider is not null)
            {
                if (collider.TryGetComponent<BoxCollider>(out BoxCollider box) && collider.TryGetComponent<PlayerController>(out PlayerController player) == false)
                {
                    if (customPhysicsNuestro.SphereRectangleCollision(box, sphereCollider))
                    {
                        Debug.Log("pelota toca borde");
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,radius);
    }
}
