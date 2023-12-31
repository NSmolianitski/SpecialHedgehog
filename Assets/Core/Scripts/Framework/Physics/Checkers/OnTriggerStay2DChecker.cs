﻿using UnityEngine;

namespace SpecialHedgehog.Framework.Physics.Checkers
{
    public class OnTriggerStay2DChecker : MonoBehaviour
    {
        private void OnTriggerStay2D(Collider2D other)
        { 
            EcsPhysicsEvents.RegisterTriggerStay2DEvent(gameObject, other);
        }
    }
}