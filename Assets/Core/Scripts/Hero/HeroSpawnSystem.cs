using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Abilities;
using SpecialHedgehog.Audio.Sounds;
using SpecialHedgehog.Cameras;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Health;
using SpecialHedgehog.Input;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.Movement;
using SpecialHedgehog.PickUps;
using SpecialHedgehog.UI;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Hero
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
        private EcsPoolInject<MobTarget> _enemyTargetPool;
        private EcsPoolInject<Health.Health> _healthPool;
        private EcsPoolInject<HealthbarViewRef> _healthbarViewRefPool;
        private EcsPoolInject<PistolAbility> _pistolAbilityPool;
        private EcsPoolInject<GemWalletOwner> _gemWalletOwnerPool;
        private EcsPoolInject<DeathSounds> _deathSoundsPool;
        private EcsPoolInject<HitSounds> _hitSoundsPool;
        
        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<UIService> _uiService;
        
        public void Init(IEcsSystems systems)
        {
            var heroView = Object.Instantiate(_gameConfig.Value.HeroViewPrefab, Vector3.zero, Quaternion.identity);
            InitComponents(heroView, systems.GetWorld());
        }

        private void InitComponents(HeroView heroView, EcsWorld world)
        {
            _heroPool.NewEntity(out var heroEntity);
            heroView.PackedEntity = world.PackEntity(heroEntity);
            
            ref var rigidbody2DRef = ref _rigidbody2DRefPool.Value.Add(heroEntity);
            rigidbody2DRef.Value = heroView.Rigidbody2D;
            
            ref var transformRef = ref _transformRefPool.Value.Add(heroEntity);
            transformRef.Value = heroView.transform;

            ref var speed = ref _speedPool.Value.Add(heroEntity);
            speed.Value = heroView.Config.HeroSpeed;

            ref var cameraTarget = ref _cameraTargetPool.Value.Add(heroEntity);
            cameraTarget.TargetTransform = heroView.transform;
            
            ref var enemyTarget = ref _enemyTargetPool.Value.Add(heroEntity);
            enemyTarget.TargetTransform = heroView.transform;
            
            ref var health = ref _healthPool.Value.Add(heroEntity);
            health.Max = heroView.Config.HeroHealth;
            health.Current = health.Max;

            ref var healthbarRef = ref _healthbarViewRefPool.Value.Add(heroEntity);
            healthbarRef.Value = _uiService.Value.GetScreen<PlayerHealthScreen>().Healthbar;

            ref var deathSounds = ref _deathSoundsPool.Value.Add(heroEntity);
            deathSounds.AudioClips = heroView.Config.DeathSounds;

            ref var hitSounds = ref _hitSoundsPool.Value.Add(heroEntity);
            hitSounds.AudioClips = heroView.Config.HitSounds;
            
            _inputListenerPool.Value.Add(heroEntity);
            _directionPool.Value.Add(heroEntity);
            _pistolAbilityPool.Value.Add(heroEntity);
            _gemWalletOwnerPool.Value.Add(heroEntity);
        }
    }
}