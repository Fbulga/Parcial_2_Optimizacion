using JetBrains.Annotations;
using UnityEngine;

namespace Structs
{
    public struct PowerUpUIElements
    {
        public GameObject text;
        [CanBeNull] public GameObject timer;

        public PowerUpUIElements(GameObject text, [CanBeNull] GameObject timer)
        {
            this.text = text;
            if (timer != null)
            {
                this.timer = timer;
            }
            else
            {
                this.timer = null;
            }
        }
    }
}