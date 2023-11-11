using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnTriggerEnter2DEvent
    {
        public GameObject SenderGameObject;
        public Collider2D Collider2D;
    }
}