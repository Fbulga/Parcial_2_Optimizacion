using UnityEngine;

namespace Scriptables
{
    [CreateAssetMenu(fileName = "BrickData",menuName = "Brick/BrickData")]
    public class BrickData : ScriptableObject
    {
        [SerializeField] private AudioClip clip;
        
        public AudioClip Clip => clip;
    }
}