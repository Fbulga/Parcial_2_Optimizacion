using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour, IPhysics, ISphere
{
    private CustomPhysicsNuestro customPhysicsNuestro;

    [SerializeField] private float radius;
    private Collider[] colliders = new Collider[1];

    private SphereCollider sphereCollider;
    // Start is called before the first frame update
    void Start()
    {
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, radius, colliders);
        if (!GameManager.Instance.ballOnBoard)
        {
            CheckCollisions();
        }
    }

    void CheckCollisions()
    {
        foreach (var collider in colliders)
        {
            if (collider is not null)
            {
                if (collider.TryGetComponent<BoxCollider>(out BoxCollider box) && collider.TryGetComponent<PlayerController>(out PlayerController player) == false)
                {
                    if (customPhysicsNuestro.SphereRectangleCollision(box, sphereCollider))
                    {
                        //Teletransportar pelota
                        customPhysicsNuestro.velocity *= -1f;
                        Debug.Log("pelota toca borde");
                    }
                }
                if (collider.TryGetComponent<PlayerController>(out PlayerController paddle))
                {
                    if (customPhysicsNuestro.SphereRectangleCollision(box, sphereCollider))
                    {
                        //Teletransportar pelota
                        customPhysicsNuestro.velocity = new Vector3(box.gameObject.transform.position.x-transform.position.x,10f,0f);
                        //customPhysicsNuestro.ApplyImpulse(Vector3.Normalize(new Vector3(box.gameObject.transform.position.x-transform.position.x,1f,0f)));
                        Debug.Log("pelota toca paleta");
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
