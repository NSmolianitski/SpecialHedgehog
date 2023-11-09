﻿using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Events
{
    public struct OnCollisionStayEvent
    {
        public GameObject SenderGameObject;
        public Collider Collider;
        public ContactPoint FirstContactPoint;
        public Vector3 RelativeVelocity;
    }
}