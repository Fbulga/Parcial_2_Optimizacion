using System;
using DefaultNamespace;
using Interfaces;
using UnityEngine;

public class PlayerController : CustomBehaviour, IPhysics, IRectangle
{

    [SerializeField] private float acceleration;
    [SerializeField] private float limits;
    [SerializeField] private BallController ball;
    [SerializeField] private float detectionRadius;
    public Transform ballPos;
    
    private CustomPhysicsNuestro customPhysicsNuestroBall;
    private bool a;
    private float InputPlayer;
    private Collider[] colliders = new Collider[8];
    private CustomPhysicsNuestro customPhysicsNuestro;
    private Collider playerCollider;
    public Vector3 Velocity => customPhysicsNuestro.velocity;
    
    protected override void CustomStart()
    {
        playerCollider = gameObject.GetComponent<BoxCollider>();
        customPhysicsNuestro = gameObject.GetComponent<CustomPhysicsNuestro>();
        colliders = new Collider[4];
        GameManager.Instance.SetPLayerInstance(this);
        customPhysicsNuestroBall = ball.GetComponent<CustomPhysicsNuestro>();
    }
    protected override void CustomUpdate()
    {
        InputPlayer = Input.GetAxis("Horizontal");
    }
    protected override void CustomFixedUpdate()
    {
        if (GameManager.Instance.ballOnBoard && Input.GetKey(KeyCode.Space))
        { 
            ShootBall();
        } 
        Movement();
    }

    protected override void CustomLateUpdate()
    {
        Physics.OverlapSphereNonAlloc(transform.position, detectionRadius, colliders);
        DetectCollision(colliders);
    }


    private void Movement()
    {
        if (InputPlayer != 0)
        {
            if (a)
            {
                customPhysicsNuestro.ApplyImpulse(new Vector3(InputPlayer,0,0)*(acceleration * 1.5f));
                a = false;
            }
            customPhysicsNuestro.ApplyForce(new Vector3(InputPlayer,0,0)*acceleration);
        }
        else
        {
            a = true;
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
        ball.ReUseMe();
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
            else if (collider is SphereCollider)
            {
                if(collider.TryGetComponent<IPowerUp>(out IPowerUp powerUp)) powerUp.UsePowerUp();
            }
        }
    }
}
