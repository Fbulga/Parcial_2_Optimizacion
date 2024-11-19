using System;
using DefaultNamespace;
using UnityEngine;

public class PlayerController : CustomBehaviour, IPhysics, IRectangle
{

    [SerializeField] private float acceleration;
    [SerializeField] private float limits;
    [SerializeField] private BallController ball;
    [SerializeField] private float detectionRadius;
    private bool a;
    private float InputPlayer;


    public static Action ReleaseBall;

    private Collider[] colliders = new Collider[8];
    private CustomPhysicsNuestro customPhysicsNuestro;
    private Collider playerCollider;
    public Vector3 Velocity => customPhysicsNuestro.velocity;
    
    protected override void CustomStart()
    {
        playerCollider = this.gameObject.GetComponent<BoxCollider>();
        customPhysicsNuestro = this.gameObject.GetComponent<CustomPhysicsNuestro>();
        ReleaseBall += ShootBall;
        colliders = new Collider[4];
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
                customPhysicsNuestro.ApplyImpulse(new Vector3(InputPlayer,0,0)*(acceleration * 1f));
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
        Unparent(ball.gameObject);
        ball.GetComponent<CustomPhysicsNuestro>().ApplyImpulse(new Vector3(0,1,0) * ball.MaxSpeed);
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
                if (collider.gameObject != gameObject && customPhysicsNuestro.RectangleCollision(playerCollider,collider))
                {
                    customPhysicsNuestro.velocity = Vector3.zero;
                    
                    // Calcula la dirección de la colisión
                    float directionX = Mathf.Sign(transform.position.x - collider.transform.position.x);

                    // Calcula la distancia de separación horizontalmente, tomando en cuenta la mitad del ancho de cada colisionador
                    float distanceToMove = playerCollider.bounds.extents.x + collider.bounds.extents.x;

                    // Ajusta la posición en el borde de colisión, tomando en cuenta la mitad del ancho o alto en la dirección de la colisión
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
