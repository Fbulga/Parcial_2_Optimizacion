using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "BallData",menuName = "Ball/BallData")]
    public class BallData : ScriptableObject
    {
        [SerializeField] private float radius;
        [SerializeField] private float playerInfluenceFactor;
        [SerializeField] private float maxSpeed;
        
        public float Radius => radius;
        public float PlayerInfluenceFactor => playerInfluenceFactor;
        public float MaxSpeed  => maxSpeed;

        public void SetMaxSpeed(float _maxSpeed)
        {
            maxSpeed = _maxSpeed;
        }
    }
}