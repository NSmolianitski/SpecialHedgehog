﻿using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics.Checkers
{
    public class OnCollisionStayChecker : MonoBehaviour
    {
        private void OnCollisionStay(Collision other)
        {
            EcsPhysicsEvents.RegisterCollisionStayEvent(gameObject, other.collider, other.GetContact(0), other.relativeVelocity);
        }
    }
}