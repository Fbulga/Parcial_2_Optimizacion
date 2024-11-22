using UnityEngine;

namespace Structs
{
    public struct CollisionResponseDto
    {
        public Vector2 closestPoint;
        public bool isTouching;
        public Vector2 collisionNormal;
    }
}