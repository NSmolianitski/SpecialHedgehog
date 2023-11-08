using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Movement;
using SpecialHedgehog.Scripts.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Enemies
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        // private EcsFilterInject<Inc<Spawn>> _enemyFilter;

        private EcsPoolInject<Enemy> _enemyPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<Speed> _speedPool;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<Rigidbody2DRef> _rigidbody2DRef;

        private EcsWorld _world;
        
        private Transform[] _enemySpawnPoints;
        private Transform _enemyParent;

        private EcsCustomInject<SceneData> _sceneData;
        private EcsCustomInject<GameConfig> _gameConfig;
        
        public void Init(IEcsSystems systems)
        {
            _world = systems.GetWorld();

            _enemySpawnPoints = _sceneData.Value.EnemySpawnPoints;
        }
        
        public void Run(IEcsSystems systems)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                SpawnEnemy();
        }

        private void SpawnEnemy()
        {
            var enemyView = Object.Instantiate(_gameConfig.Value.EnemyViewPrefab, _enemySpawnPoints.GetRandom().position,
                Quaternion.identity, _enemyParent);

            var enemyEntity = _world.NewEntity();
            enemyView.PackedEntity = _world.PackEntity(enemyEntity);

            ref var transformRef = ref _transformRefPool.Value.Add(enemyEntity);
            transformRef.Value = enemyView.transform;
            
            ref var speed = ref _speedPool.Value.Add(enemyEntity);
            speed.Value = _gameConfig.Value.EnemySpeed;

            ref var rigidbody2DRef = ref _rigidbody2DRef.Value.Add(enemyEntity);
            rigidbody2DRef.Value = enemyView.Rigidbody2D;
            
            _enemyPool.Value.Add(enemyEntity);
            _directionPool.Value.Add(enemyEntity);
        }
    }
}