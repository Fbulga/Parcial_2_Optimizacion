// using Interfaces;
// using UnityEngine;
//
// namespace DefaultNamespace
// {
//     public class MoreBallsPowerUp : MonoBehaviour, IPowerUp
//
//     {
//         public void UsePowerUp()
//         {
//             var ball = BallPool.Instance.GetBall();
//             TryGetComponent<BallController>(out BallController ballController);
//             if (ballController != null)
//             {
//                 ballController.ImpulseMe();
//             }
//         }
//     }
// }