using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Death;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Physics.Events;
using SpecialHedgehog.Mobs;

namespace SpecialHedgehog.Projectiles
{
    public class ProjectileMobTriggerSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<OnTriggerEnter2DEvent>> _triggerEnterEventFilter = Constants.Worlds.Events;

        private EcsPoolInject<OnTriggerEnter2DEvent> _onTriggerEnterEventPool = Constants.Worlds.Events;
        
        private EcsPoolInject<MakeDamageRequest> _makeDamageRequestPool = Constants.Worlds.Events;
        private EcsPoolInject<DamageStat> _damageStatPool;
        private EcsPoolInject<DieAfterTime> _dieAfterTimePool;
        private EcsPoolInject<Dead> _deadPool;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _triggerEnterEventFilter.Value)
            {
                ref var triggerEvent = ref _onTriggerEnterEventPool.Value.Get(entity);
                
                if (!triggerEvent.SenderGameObject.TryGetComponentInParent<ProjectileView>(out var projectileView)) continue;
                if (!projectileView.PackedEntity.Unpack(_world.Value, out var projectileEntity)) continue;

                if (!triggerEvent.Collider2D.gameObject.TryGetComponentInParent<MobView>(out var mobView)) continue;
                if (!mobView.PackedEntity.Unpack(_world.Value, out var mobEntity)) continue;

                _dieAfterTimePool.Value.Del(projectileEntity);
                _deadPool.Value.Add(projectileEntity);

                _onTriggerEnterEventPool.Value.Del(entity);
                
                ref var makeDamageRequest = ref _makeDamageRequestPool.NewEntity(out _);
                makeDamageRequest.Dealer = projectileView.PackedEntity;
                makeDamageRequest.Target = mobView.PackedEntity;

                ref var damageStat = ref _damageStatPool.Value.Get(projectileEntity);
                makeDamageRequest.Value = damageStat.CurrentValue;
            }
        }
    }
}