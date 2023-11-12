using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Attack;
using SpecialHedgehog.Audio.Sounds;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Movement;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Mobs
{
    public class MobSpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        // private EcsFilterInject<Inc<Spawn>> _enemyFilter;

        private EcsPoolInject<Mob> _mobPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<Speed> _speedPool;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<Rigidbody2DRef> _rigidbody2DRefPool;
        private EcsPoolInject<DamageStat> _damageStatPool;
        private EcsPoolInject<Health.Health> _healthPool;
        private EcsPoolInject<DeathSounds> _deathSoundsPool;
        private EcsPoolInject<AttackCooldown> _attackCooldownPool;

        private EcsWorld _world;
        
        private Transform[] _mobSpawnPoints;

        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<GameConfig> _gameConfig;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _mobSpawnPoints = _sceneData.Value.MobSpawnPoints;
        }

#if UNITY_EDITOR
        public void Run(IEcsSystems systems)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                SpawnEnemy();
        }
#endif

        private void SpawnEnemy()
        {
            var mobView = Object.Instantiate(_gameConfig.Value.MobViewPrefab, _mobSpawnPoints.GetRandom().position,
                Quaternion.identity, _sceneData.Value.MobParent);

            var mobEntity = _world.NewEntity();
            mobView.PackedEntity = _world.PackEntity(mobEntity);

            ref var transformRef = ref _transformRefPool.Value.Add(mobEntity);
            transformRef.Value = mobView.transform;
            
            ref var speed = ref _speedPool.Value.Add(mobEntity);
            speed.Value = mobView.Config.Speed;

            ref var rigidbody2DRef = ref _rigidbody2DRefPool.Value.Add(mobEntity);
            rigidbody2DRef.Value = mobView.Rigidbody2D;

            ref var damageStat = ref _damageStatPool.Value.Add(mobEntity);
            damageStat.InitValue = mobView.Config.Damage;
            damageStat.CurrentValue = damageStat.InitValue;

            ref var health = ref _healthPool.Value.Add(mobEntity);
            health.Max = mobView.Config.Health;
            health.Current = health.Max;

            ref var deathSounds = ref _deathSoundsPool.Value.Add(mobEntity);
            deathSounds.AudioClips = mobView.Config.DeathSounds;

            ref var attackCooldown = ref _attackCooldownPool.Value.Add(mobEntity);
            attackCooldown.CooldownTime = mobView.Config.AttackCooldown;
            
            _mobPool.Value.Add(mobEntity);
            _directionPool.Value.Add(mobEntity);
        }
    }
}