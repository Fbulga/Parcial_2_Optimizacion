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

    private Collider[] colliders = new Collider[2];
    private CustomPhysicsNuestro customPhysicsNuestro;
    private Collider playerCollider;
    public Vector3 Velocity => customPhysicsNuestro.velocity;
    
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
    }

    private void LateUpdate()
    {
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
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }
    
}
