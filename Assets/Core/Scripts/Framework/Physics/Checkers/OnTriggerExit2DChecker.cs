using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnTriggerExit2DChecker : MonoBehaviour
    {
        private void OnTriggerExit2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerExit2DEvent(gameObject, other);
        }
    }
}