using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnCollisionEnterEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
        public ContactPoint FirstContactPoint;
        public Vector3 RelativeVelocity;
    }
}