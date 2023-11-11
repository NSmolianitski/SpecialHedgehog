using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using SpecialHedgehog.Framework;
using UnityEngine;

namespace SpecialHedgehog.Projectiles
{
    public class ProjectileView : EntityOwner
    {
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponentInParent<EntityOwner>(out var entityOwner))
                return;
            
            OnEntityOwnerTriggerEnter(entityOwner);
        }

        private void OnEntityOwnerTriggerEnter(EntityOwner entityOwner)
        {
            var world = EcsStartup.Instance.GetWorld(null);

            PackedEntity.Unpack(world, out var projectileEntity);

            var firstEnemyHitPool = world.GetPool<FirstEnemyHit>();
            if (firstEnemyHitPool.Has(projectileEntity))
                return;

            ref var hitEnemy = ref firstEnemyHitPool.Add(projectileEntity);
            hitEnemy.HitEnemyPackedEntity = entityOwner.PackedEntity;
        }
    }
}