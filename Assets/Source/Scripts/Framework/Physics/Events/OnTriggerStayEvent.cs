using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnTriggerStayEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}