using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnTriggerEnterEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
    }
}