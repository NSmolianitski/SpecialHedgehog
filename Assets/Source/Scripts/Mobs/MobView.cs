using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Hero;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Mobs
{
    public class MobView : EntityOwner
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponentInParent<HeroView>(out var heroView))
                return;
            
            OnHeroTriggerEnter(heroView);
        }

        private void OnHeroTriggerEnter(HeroView heroView)
        {
            var world = EcsStartup.Instance.GetWorld(null);

            PackedEntity.Unpack(world, out var mobEntity);

            var enemyNearbyPool = world.GetPool<EnemyNearby>();
            if (enemyNearbyPool.Has(mobEntity))
                return;
            
            ref var enemyNearby = ref enemyNearbyPool.Add(mobEntity);
            enemyNearby.EnemyPackedEntity = heroView.PackedEntity;
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponentInParent<HeroView>(out var heroView))
                return;
            
            OnHeroTriggerExit();
        }

        private void OnHeroTriggerExit()
        {
            var world = EcsStartup.Instance.GetWorld(null);

            PackedEntity.Unpack(world, out var mobEntity);

            var enemyNearbyPool = world.GetPool<EnemyNearby>();
            if (!enemyNearbyPool.Has(mobEntity))
                return;
            
            enemyNearbyPool.Del(mobEntity);
        }
    }
}