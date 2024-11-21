using System;
using DefaultNamespace;
using Interfaces;
using UnityEngine;

public class PlayerController : CustomBehaviour, IPhysics, IRectangle
{
    public Transform ballPos;
    [SerializeField] private BallController ball;
    [SerializeField] private CustomPhysicsNuestro customPhysicsNuestro;
    [SerializeField] private Collider playerCollider;
    [SerializeField] private CustomPhysicsNuestro customPhysicsNuestroBall;
    [SerializeField] private float acceleration;
    [SerializeField] private float detectionRadius;
    
    private bool firstImpulse;
    private float InputPlayer;
    private Collider[] colliders = new Collider[8];

    public Vector3 Velocity => customPhysicsNuestro.velocity;
    
    protected override void CustomStart()
    {
        colliders = new Collider[4];
        GameManager.Instance.SetPLayerInstance(this);
    }
    protected override void CustomUpdate()
    {
        InputPlayer = Input.GetAxis("Horizontal");
    }
    protected override void CustomFixedUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders);
        if (GameManager.Instance.ballOnBoard && Input.GetKey(KeyCode.Space))
        { 
            ShootBall();
        } 
        Movement();
    }

    protected override void CustomLateUpdate()
    {
        DetectCollision(colliders);
    }


    private void Movement()
    {
        if (InputPlayer != 0)
        {
            if (firstImpulse)
            {
                customPhysicsNuestro.ApplyImpulse(new Vector3(InputPlayer,0,0)*(acceleration * 1.5f));
                firstImpulse = false;
            }
            customPhysicsNuestro.ApplyForce(new Vector3(InputPlayer,0,0)*acceleration);
        }
        else
        {
            firstImpulse = true;
        }
        customPhysicsNuestro.ApplyFriction();
    }
    
    private void ShootBall()
    {
        Unparent();
        customPhysicsNuestroBall.ApplyImpulse(new Vector3(0,1,0) * ball.MaxSpeed);
        GameManager.Instance.ballOnBoard = false;
    }
    
    private void Unparent()
    {
        ball.transform.SetParent(null);
    }
    public void Parent()
    {
        ball.ReUseMe(ballPos.position);
        ball.gameObject.SetActive(true);
        ball.transform.SetParent(gameObject.transform);
        GameManager.Instance.ActiveBallsUp();
        
    }
    private void DetectCollision(Collider[] colliders)
    {
        foreach (Collider collider in colliders)
        {
            if (collider is BoxCollider)
            {
                if (collider.gameObject != gameObject && customPhysicsNuestro.RectangleCollision(playerCollider,collider))
                {
                    customPhysicsNuestro.velocity = Vector3.zero;
                    
                    float directionX = Mathf.Sign(transform.position.x - collider.transform.position.x);
                    
                    float distanceToMove = playerCollider.bounds.extents.x + collider.bounds.extents.x;
                    
                    transform.position = new Vector3(
                        collider.transform.position.x + directionX * distanceToMove,
                        transform.position.y,
                        transform.position.z
                    );
                }
            }
        }
    }
}
