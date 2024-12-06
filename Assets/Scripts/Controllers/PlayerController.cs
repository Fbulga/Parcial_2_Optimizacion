using Interfaces;
using Managers;
using PhysicsOur;
using UnityEngine;

namespace Controllers
{
    public class PlayerController : CustomBehaviour, IPhysics
    {
        public Transform ballPos;
        [SerializeField] private BallController ball;
        [SerializeField] private CustomPhysicsNuestro customPhysicsNuestro;
        [SerializeField] private Collider playerCollider;
        [SerializeField] private CustomPhysicsNuestro customPhysicsNuestroBall;
        [SerializeField] private float acceleration;
        [SerializeField] private float detectionRadius;
        [SerializeField] private float longerBarTime;
        [SerializeField] private float slowerBallTime;
        private float baseDetectionRadius;
    
        private float timer;
        private float timerBalls;
        private bool isLongerBar;
        private bool isSlowerBalls = false;
        private bool firstImpulse;
        private float inputPlayer;
        private Collider[] colliders = new Collider[8];

        public Vector3 Velocity => customPhysicsNuestro.velocity;
    
        protected override void CustomStart()
        {
            colliders = new Collider[4];
            GameManager.Instance.SetPLayerInstance(this);
            baseDetectionRadius = detectionRadius;
        }
        protected override void CustomUpdate()
        {
            inputPlayer = Input.GetAxis("Horizontal");
            MakeBarNormalAfterTime();
            MakeBallSpeedNormalAfterTime();
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
            if (inputPlayer != 0)
            {
                if (firstImpulse)
                {
                    customPhysicsNuestro.ApplyImpulse(new Vector3(inputPlayer,0,0)*(acceleration * 1.75f));
                    firstImpulse = false;
                }
                customPhysicsNuestro.ApplyForce(new Vector3(inputPlayer,0,0)*acceleration);
            }
            else
            {
                firstImpulse = true;
            }
            
            if (Mathf.Abs(inputPlayer) <= 0.25f)
            {
                customPhysicsNuestro.velocity *= 0.75f;
            }
            customPhysicsNuestro.ApplyFriction();
        }
    
        private void ShootBall()
        {
            ball.transform.SetParent(null);
            customPhysicsNuestroBall.ApplyImpulse(new Vector3(0,1,0) * ball.MaxSpeed);
            GameManager.Instance.ballOnBoard = false;
        }
        
        public void Parent()
        {
            ball.ReUseMe(ballPos.position);
            ball.gameObject.SetActive(true);
            ball.transform.SetParent(transform);
        }
        public void ChangeBallSpeed()
        {
            isSlowerBalls = true;
            timerBalls = 0;
        }
        public void LongerBar(bool isBallOn)
        {
            timer = 0;
            isLongerBar = true;
            detectionRadius *= 1.5f;
            if (isBallOn)
            {
                ball.transform.SetParent(null);
                transform.localScale = new Vector3(1.5f,1,1);
                ball.transform.SetParent(transform);
            }
            else
            {
                transform.localScale = new Vector3(1.5f,1,1);
            }
            

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
        private void MakeBarNormalAfterTime()
        {
            if (isLongerBar)
            {
                timer += Time.deltaTime;
                if (timer >= longerBarTime)
                {
                    isLongerBar = false;
                    detectionRadius = baseDetectionRadius;
                    if (GameManager.Instance.ballOnBoard)
                    {
                        ball.transform.SetParent(null);
                        transform.localScale = new Vector3(1,1,1);
                        ball.transform.SetParent(transform);
                    }
                    else
                    {
                        transform.localScale = new Vector3(1,1,1);
                    }
                }
            }
        }
        private void MakeBallSpeedNormalAfterTime()
        {
            if (isSlowerBalls)
            {
                timerBalls += Time.deltaTime;
                if (timerBalls >= slowerBallTime)
                {
                    isSlowerBalls = false;
                    GameManager.Instance.ResetBallsMaxSpeed();
                }
            }
        }
    }
}
