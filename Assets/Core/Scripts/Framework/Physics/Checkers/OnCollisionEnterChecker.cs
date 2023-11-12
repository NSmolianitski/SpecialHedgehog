using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnCollisionEnterChecker : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            EcsPhysicsEvents.RegisterCollisionEnterEvent(gameObject, other.collider, other.GetContact(0), other.relativeVelocity);
        }
    }
}