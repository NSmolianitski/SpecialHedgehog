using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Checkers
{
    public class OnTriggerExitChecker : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            EcsPhysicsEvents.RegisterTriggerExitEvent(gameObject, other);
        }
    }
}