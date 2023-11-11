using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnCollisionExitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
        public Vector3 RelativeVelocity;
    }
}