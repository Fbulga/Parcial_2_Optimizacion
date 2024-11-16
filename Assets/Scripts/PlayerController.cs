using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IPhysics, IRectangle
{

    [SerializeField] private float acceleration;
    [SerializeField] private float limits;
    [SerializeField] private GameObject ball;
    [SerializeField] private float ballImpulse;
    [SerializeField] private float detectionRadius;
    [SerializeField] private GameObject leftPoint;
    [SerializeField] private GameObject rightPoint;


    public static Action ReleaseBall;

    private Collider[] colliders = new Collider[1];
    private CustomPhysicsNuestro customPhysicsNuestro;
    private Collider playerCollider;
    
    private void Start()
    {
        playerCollider = this.gameObject.GetComponent<BoxCollider>();
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        ReleaseBall += ShootBall;
    }
    
    void FixedUpdate()
    {
        if (GameManager.Instance.ballOnBoard && Input.GetKey(KeyCode.Space))
        { 
            ShootBall();
        } 
        Movement();

        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders);
        DetectCollision(colliders);
    }

   
    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            customPhysicsNuestro.ApplyForce(new Vector3(Input.GetAxis("Horizontal"),0,0)*acceleration);
        }
        customPhysicsNuestro.ApplyFriction(); 
        //CheckBorder();
    }
    
    private void ShootBall()
    {
        Unparent(ball);
        ball.GetComponent<CustomPhysicsNuestro>().ApplyImpulse(new Vector3(0,1,0) * ballImpulse);
        GameManager.Instance.ballOnBoard = false;
    }
    
    
    private void Unparent(GameObject ballGameObject)
    {
        ballGameObject.transform.SetParent(null);
    }

    private void DetectCollision(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider is BoxCollider)
            {
                if (customPhysicsNuestro.RectangleCollision(playerCollider,collider) && collider.gameObject != this.gameObject)
                {
                    customPhysicsNuestro.velocity = new Vector3(0, 0, 0);
                    if (transform.position.x < 0f)
                    {
                        transform.position = new Vector3(leftPoint.transform.position.x, transform.position.y, transform.position.z);
                    }
                    else
                    {
                        transform.position = new Vector3(rightPoint.transform.position.x, transform.position.y, transform.position.z);
                    }
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }
    
}
