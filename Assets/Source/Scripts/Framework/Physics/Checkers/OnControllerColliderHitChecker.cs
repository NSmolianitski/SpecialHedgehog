using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Checkers
{
    public class OnControllerColliderHitChecker : MonoBehaviour
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            EcsPhysicsEvents.RegisterControllerColliderHitEvent(gameObject, hit.collider, hit.normal, hit.moveDirection);
        }
    }
}