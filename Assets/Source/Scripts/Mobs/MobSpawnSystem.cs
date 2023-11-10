using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Damage;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Movement;
using SpecialHedgehog.Scripts.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Mobs
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

        private EcsWorld _world;
        
        private Transform[] _mobSpawnPoints;
        private Transform _mobParent;

        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<GameConfig> _gameConfig;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _mobSpawnPoints = _sceneData.Value.MobSpawnPoints;
        }
        
        public void Run(IEcsSystems systems)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var enemyView = Object.Instantiate(_gameConfig.Value.MobViewPrefab, _mobSpawnPoints.GetRandom().position,
                Quaternion.identity, _mobParent);

            var mobEntity = _world.NewEntity();
            enemyView.PackedEntity = _world.PackEntity(mobEntity);

            ref var transformRef = ref _transformRefPool.Value.Add(mobEntity);
            transformRef.Value = enemyView.transform;
            
            ref var speed = ref _speedPool.Value.Add(mobEntity);
            speed.Value = _gameConfig.Value.MobSpeed;

            ref var rigidbody2DRef = ref _rigidbody2DRefPool.Value.Add(mobEntity);
            rigidbody2DRef.Value = enemyView.Rigidbody2D;

            ref var damageStat = ref _damageStatPool.Value.Add(mobEntity);
            damageStat.InitValue = _gameConfig.Value.MobDamage;
            damageStat.CurrentValue = damageStat.InitValue;

            ref var health = ref _healthPool.Value.Add(mobEntity);
            health.Max = _gameConfig.Value.MobHealth;
            health.Current = health.Max;
            
            _mobPool.Value.Add(mobEntity);
            _directionPool.Value.Add(mobEntity);
        }
    }
}