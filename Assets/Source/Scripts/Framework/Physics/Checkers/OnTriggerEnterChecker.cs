using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Checkers
{
    public class OnTriggerEnterChecker : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        { 
            EcsPhysicsEvents.RegisterTriggerEnterEvent(gameObject, other);
        }
    }
}