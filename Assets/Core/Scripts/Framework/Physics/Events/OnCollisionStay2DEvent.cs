using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnCollisionStay2DEvent
    {
        public GameObject SenderGameObject;
        public Collider2D Collider2D;
        public ContactPoint2D FirstContactPoint2D;
        public Vector2 RelativeVelocity;
    }
}