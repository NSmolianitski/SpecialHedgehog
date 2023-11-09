using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnTriggerExitEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}