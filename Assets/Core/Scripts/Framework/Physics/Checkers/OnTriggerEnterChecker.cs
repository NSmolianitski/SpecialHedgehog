using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnTriggerEnterChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        { 
            EcsPhysicsEvents.RegisterTriggerEnterEvent(gameObject, other);
        }
    }
}