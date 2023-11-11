using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnTriggerEnterEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}