using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnTriggerStay2DEvent
    {
        public GameObject SenderGameObject;
        public Collider2D Collider2D;
    }
}