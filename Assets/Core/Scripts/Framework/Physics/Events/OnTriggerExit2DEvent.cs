﻿using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Events
{
    public struct OnTriggerExit2DEvent
    {
        public GameObject SenderGameObject;
        public Collider2D Collider2D;
    }
}