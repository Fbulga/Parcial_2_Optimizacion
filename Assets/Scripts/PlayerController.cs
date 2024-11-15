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


    public static Action ReleaseBall;

    private Collider[] colliders = new Collider[5];
    private CustomPhysicsNuestro customPhysicsNuestro;
    private Collider playerCollider;
    
    private void Start()
    {
        playerCollider = this.gameObject.GetComponent<BoxCollider>();
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        ReleaseBall += ShootBall;
    }
    
    void Update()
    {
        if (GameManager.Instance.ballOnBoard && Input.GetKey(KeyCode.Space))
        { 
            ShootBall();
        } 
        Movement();

        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders);
        DetectCollision(colliders);
    }
    
    private void CheckBorder()
    {
        if (transform.position.x < -limits)
        {
            customPhysicsNuestro.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(-limits, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > limits)
        {
            customPhysicsNuestro.velocity = new Vector3(0, 0, 0);
            transform.position = new Vector3(limits, transform.position.y, transform.position.z);
        }
    }
    
    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            customPhysicsNuestro.ApplyForce(new Vector3(Input.GetAxis("Horizontal"),0,0)*acceleration);
        }
        customPhysicsNuestro.ApplyFriction(); 
        CheckBorder();
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
            if (collider is ISphere)
            {
                if (customPhysicsNuestro.SphereRectangleCollision(playerCollider,collider.GetComponent<SphereCollider>()))
                {
                    //var colliderPhysics = collider.GetComponent<CustomPhysicsNuestro>();
                    Debug.Log("pelota toca paleta");
                    //colliderPhysics.ApplyImpulse(-colliderPhysics.velocity);
                }
            }
        }
    }
    
}
