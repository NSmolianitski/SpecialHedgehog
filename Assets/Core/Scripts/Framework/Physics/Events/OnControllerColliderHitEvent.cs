using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnControllerColliderHitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
        public Vector3 HitNormal;
        public Vector3 MoveDirection;
    }
}