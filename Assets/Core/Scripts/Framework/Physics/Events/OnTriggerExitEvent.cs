using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnTriggerExitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}