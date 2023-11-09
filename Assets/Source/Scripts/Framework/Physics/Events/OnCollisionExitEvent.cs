using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnCollisionExitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
        public Vector3 RelativeVelocity;
    }
}