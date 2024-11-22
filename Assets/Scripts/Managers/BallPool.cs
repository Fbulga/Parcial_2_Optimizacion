using System.Collections.Generic;
using Controllers;
using PhysicsOur;
using UnityEngine;

namespace Managers
{
    public class BallPool : MonoBehaviour
    {
        [SerializeField] private GameObject ballPrefab;
        private Queue<BallController> pool = new Queue<BallController>();
        [SerializeField] private PhysicsEngine physicsEngine;
        [SerializeField] private PlayerController player;
        [SerializeField] private BallController playerBall;
        
        public static BallPool Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public BallController GetBall()
        {
            if (pool.Count > 0)
            {
                BallController ball = pool.Dequeue();
                ball.transform.position = player.ballPos.position;
                ball.transform.SetParent(player.transform);
                ball.gameObject.SetActive(true);
                GameManager.Instance.OnBallsUp?.Invoke();
                return ball;
            }
            else
            {
                var ball =Instantiate(ballPrefab, player.ballPos.position, Quaternion.identity, player.transform);
                ball.TryGetComponent<BallController>(out BallController ballController);
                GameManager.Instance.OnBallsUp?.Invoke();
                physicsEngine.AddObjet(ball);
                return ballController;
            }
        }

        public void ReturnBall(BallController ball)
        {
            ball.gameObject.SetActive(false);
            if (ball == playerBall) return;
            pool.Enqueue(ball);
        }
    }
}