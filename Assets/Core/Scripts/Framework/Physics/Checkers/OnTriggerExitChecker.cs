using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnTriggerExitChecker : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            EcsPhysicsEvents.RegisterTriggerExitEvent(gameObject, other);
        }
    }
}