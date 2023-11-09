using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Checkers
{
    public class OnTriggerStayChecker : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            EcsPhysicsEvents.RegisterTriggerStayEvent(gameObject, other);
        }
    }
}