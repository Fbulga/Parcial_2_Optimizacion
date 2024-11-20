// using System.Collections.Generic;
// using UnityEngine;
//
// namespace DefaultNamespace
// {
//     public class BallPool : MonoBehaviour
//     {
//         [SerializeField] private GameObject ballPrefab;
//         private Queue<GameObject> pool = new Queue<GameObject>();
//         [SerializeField] private PhysicsEngine physicsEngine;
//         [SerializeField] private PlayerController player;
//         
//         public static BallPool Instance { get; private set; }
//
//         private void Awake()
//         {
//             if (Instance == null)
//             {
//                 Instance = this;
//             }
//             else
//             {
//                 Destroy(gameObject);
//                 return;
//             }
//         }
//
//         public GameObject GetBall()
//         {
//             if (pool.Count > 0)
//             {
//                 GameObject ball = pool.Dequeue();
//                 ball.SetActive(true);
//                 GameManager.Instance.ActiveBallsUp();
//                 return ball;
//             }
//             else
//             {
//                 var ball =Instantiate(ballPrefab, player.ballPos.position, Quaternion.identity, player.transform);
//                 GameManager.Instance.ActiveBallsUp();
//                 physicsEngine.AddObjet(ball);
//                 return ball;
//             }
//         }
//
//         public void ReturnBall(GameObject ball)
//         {
//             ball.SetActive(false);
//             pool.Enqueue(ball);
//         }
//     }
// }