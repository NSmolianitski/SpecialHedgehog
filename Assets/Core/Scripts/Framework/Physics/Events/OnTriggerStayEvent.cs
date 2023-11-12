using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnTriggerStayEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}