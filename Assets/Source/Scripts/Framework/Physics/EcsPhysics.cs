using Leopotam.EcsLite;
using SpecialHedgehog.Scripts.Framework.Physics.Events;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework.Physics
{
    public static class EcsPhysicsEvents
    {
        public static EcsWorld EcsWorld;

        public static void RegisterTriggerEnterEvent(GameObject senderGameObject, Collider collider)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerEnterEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider = collider;
        }

        public static void RegisterTriggerStayEvent(GameObject senderGameObject, Collider collider)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerStayEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider = collider;
        }

        public static void RegisterTriggerExitEvent(GameObject senderGameObject, Collider collider)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerExitEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider = collider;
        }

        public static void RegisterCollisionEnterEvent(GameObject senderGameObject, Collider collider, ContactPoint firstContactPoint, Vector3 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionEnterEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider = collider;
            collisionEvent.FirstContactPoint = firstContactPoint;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterCollisionStayEvent(GameObject senderGameObject, Collider collider, ContactPoint firstContactPoint, Vector3 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionStayEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider = collider;
            collisionEvent.FirstContactPoint = firstContactPoint;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterCollisionExitEvent(GameObject senderGameObject, Collider collider, Vector3 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionExitEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider = collider;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterControllerColliderHitEvent(GameObject senderGameObject, Collider collider, Vector3 hitNormal, Vector3 moveDirection)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnControllerColliderHitEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider = collider;
            collisionEvent.HitNormal = hitNormal;
            collisionEvent.MoveDirection = moveDirection;
        }

        public static void RegisterCollisionEnter2DEvent(GameObject senderGameObject, Collider2D collider2D, ContactPoint2D firstContactPoint2D, Vector2 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionEnter2DEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider2D = collider2D;
            collisionEvent.FirstContactPoint2D = firstContactPoint2D;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterCollisionStay2DEvent(GameObject senderGameObject, Collider2D collider2D, ContactPoint2D firstContactPoint2D, Vector2 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionStay2DEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider2D = collider2D;
            collisionEvent.FirstContactPoint2D = firstContactPoint2D;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterCollisionExit2DEvent(GameObject senderGameObject, Collider2D collider2D, Vector2 relativeVelocity)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnCollisionExit2DEvent>();
            
            ref var collisionEvent = ref pool.Add(eventEntity);
            collisionEvent.SenderGameObject = senderGameObject;
            collisionEvent.Collider2D = collider2D;
            collisionEvent.RelativeVelocity = relativeVelocity;
        }

        public static void RegisterTriggerEnter2DEvent(GameObject senderGameObject, Collider2D collider2D)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerEnter2DEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider2D = collider2D;
        }
        
        public static void RegisterTriggerStay2DEvent(GameObject senderGameObject, Collider2D collider2D)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerStay2DEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider2D = collider2D;
        }

        public static void RegisterTriggerExit2DEvent(GameObject senderGameObject, Collider2D collider2D)
        {
            var eventEntity = EcsWorld.NewEntity();
            var pool = EcsWorld.GetPool<OnTriggerExit2DEvent>();
            
            ref var triggerEvent = ref pool.Add(eventEntity);
            triggerEvent.SenderGameObject = senderGameObject;
            triggerEvent.Collider2D = collider2D;
        }
    }
}