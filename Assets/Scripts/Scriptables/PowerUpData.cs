using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "PowerUpData",menuName = "PowerUp/PowerUpData")]
    public class PowerUpData : ScriptableObject
    {
        [SerializeField] private float radius;
        [SerializeField] private AudioClip clip;
        
        public float Radius => radius;
        public AudioClip Clip => clip;
    }
}