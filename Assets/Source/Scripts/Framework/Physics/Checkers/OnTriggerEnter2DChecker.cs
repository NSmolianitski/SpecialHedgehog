using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnTriggerEnter2DChecker : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerEnter2DEvent(gameObject, other);
        }
    }
}