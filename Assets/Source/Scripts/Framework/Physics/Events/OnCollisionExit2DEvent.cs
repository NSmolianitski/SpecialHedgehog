using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnCollisionExit2DEvent
    {
        public GameObject SenderGameObject;
        public Collider2D Collider2D;
        public Vector2 RelativeVelocity;
    }
}