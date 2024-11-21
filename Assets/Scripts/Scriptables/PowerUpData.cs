using UnityEngine;

namespace DefaultNamespace.Scriptables
{
    [CreateAssetMenu(fileName = "PowerUpData",menuName = "PowerUp/PowerUpData")]
    public class PowerUpData : ScriptableObject
    {
        [SerializeField] private float radius;
        
        public float Radius => radius;
    }
}