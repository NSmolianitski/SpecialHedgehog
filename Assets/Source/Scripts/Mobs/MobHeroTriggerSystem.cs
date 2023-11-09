using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Framework.Physics.Events;
using SpecialHedgehog.Scripts.Hero;

namespace SpecialHedgehog.Scripts.Mobs
{
    public class MobHeroTriggerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnTriggerEnter2DEvent>> _triggerEnterFilter = Constants.Worlds.Events;
        private EcsFilterInject<Inc<OnTriggerExit2DEvent>> _triggerExitFilter = Constants.Worlds.Events;

        private EcsPoolInject<OnTriggerEnter2DEvent> _onTriggerEnter2DEventPool = Constants.Worlds.Events;
        private EcsPoolInject<OnTriggerExit2DEvent> _onTriggerExit2DEventPool = Constants.Worlds.Events;
        private EcsPoolInject<EnemyNearby> _enemyNearbyPool;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            OnTriggerEnter();
            OnTriggerExit();
        }

        private void OnTriggerEnter()
        {
            foreach (var entity in _triggerEnterFilter.Value)
            {
                ref var triggerEvent = ref _onTriggerEnter2DEventPool.Value.Get(entity);
                
                if (!triggerEvent.SenderGameObject.TryGetComponentInParent<MobView>(out var mobView)) continue;

                if (!mobView.PackedEntity.Unpack(_world.Value, out var mobEntity)) continue;
                if (_enemyNearbyPool.Value.Has(mobEntity)) continue;
                
                if (!triggerEvent.Collider2D.gameObject.TryGetComponentInParent<HeroView>(out var heroView)) continue;
                if (!heroView.PackedEntity.Unpack(_world.Value, out var heroEntity)) continue;

                ref var enemyNearby = ref _enemyNearbyPool.Value.Add(mobEntity);
                enemyNearby.EnemyPackedEntity = heroView.PackedEntity;
            }
        }
        
        private void OnTriggerExit()
        {
            foreach (var entity in _triggerExitFilter.Value)
            {
                ref var triggerEvent = ref _onTriggerExit2DEventPool.Value.Get(entity);
                
                if (!triggerEvent.SenderGameObject.TryGetComponentInParent<MobView>(out var mobView)) continue;
                if (!triggerEvent.Collider2D.gameObject.TryGetComponentInParent<HeroView>(out var heroView)) continue;

                if (!mobView.PackedEntity.Unpack(_world.Value, out var mobEntity)) continue;
                if (_enemyNearbyPool.Value.Has(mobEntity))
                    _enemyNearbyPool.Value.Del(mobEntity);
            }
        }
    }
}