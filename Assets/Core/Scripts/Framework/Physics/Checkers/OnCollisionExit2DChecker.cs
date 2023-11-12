using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnCollisionExit2DChecker : MonoBehaviour
    {
        private void OnCollisionExit2D(Collision2D other)
        {
            EcsPhysicsEvents.RegisterCollisionExit2DEvent(gameObject, other.collider, other.relativeVelocity);
        }
    }
}