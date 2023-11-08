using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Cameras;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Input;
using SpecialHedgehog.Scripts.Movement;
using SpecialHedgehog.Scripts.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Hero
{
    public class HeroSpawnSystem : IEcsInitSystem
    {
        private EcsPoolInject<Hero> _heroPool;
        private EcsPoolInject<Rigidbody2DRef> _rigidbody2DRefPool;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<InputListener> _inputListenerPool;
        private EcsPoolInject<Speed> _speedPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<CameraTarget> _cameraTargetPool;
        
        private EcsCustomInject<GameConfig> _gameConfig;
        
        public void Init(IEcsSystems systems)
        {
            var heroView = Object.Instantiate(_gameConfig.Value.HeroViewPrefab, Vector3.zero, Quaternion.identity);
            InitComponents(heroView);
        }

        private void InitComponents(HeroView heroView)
        {
            _heroPool.NewEntity(out var heroEntity);
            
            ref var rigidbody2DRef = ref _rigidbody2DRefPool.Value.Add(heroEntity);
            rigidbody2DRef.Value = heroView.Rigidbody2D;
            
            ref var transformRef = ref _transformRefPool.Value.Add(heroEntity);
            transformRef.Value = heroView.transform;

            ref var speed = ref _speedPool.Value.Add(heroEntity);
            speed.Value = _gameConfig.Value.HeroSpeed;

            ref var cameraTarget = ref _cameraTargetPool.Value.Add(heroEntity);
            cameraTarget.TargetTransform = heroView.transform;
            
            _inputListenerPool.Value.Add(heroEntity);
            _directionPool.Value.Add(heroEntity);
        }
    }
}