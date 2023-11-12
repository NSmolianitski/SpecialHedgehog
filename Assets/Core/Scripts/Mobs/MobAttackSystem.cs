using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Attack;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Configuration;

namespace SpecialHedgehog.Mobs
{
    public class MobAttackSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<EnemyNearby, Mob, DamageStat>, Exc<AttackCooldown>> _filter;

        private EcsPoolInject<MakeDamageRequest> _makeDamageRequestPool = Constants.Worlds.Events;
        private EcsPoolInject<DamageStat> _damageStatPool;
        private EcsPoolInject<AttackCooldown> _attackCooldownPool;
        private EcsPoolInject<EnemyNearby> _enemyNearbyPool;

        private EcsCustomInject<GameConfig> _gameConfig;
        
        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var request = ref _makeDamageRequestPool.NewEntity(out _);
                ref var damageStat = ref _damageStatPool.Value.Get(entity);
                ref var enemyNearby = ref _enemyNearbyPool.Value.Get(entity);

                request.Dealer = _world.Value.PackEntity(entity);
                request.Target = enemyNearby.EnemyPackedEntity;
                request.Value = damageStat.CurrentValue;

                ref var attackCooldown = ref _attackCooldownPool.Value.Add(entity);
                attackCooldown.TimeRemaining = _gameConfig.Value.MobAttackCooldown;
            }
        }
    }
}