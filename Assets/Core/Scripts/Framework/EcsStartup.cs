using BaboonAndCo.Patterns;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.UnityEditor;
using SpecialHedgehog.Abilities;
using SpecialHedgehog.Attack;
using SpecialHedgehog.Audio;
using SpecialHedgehog.Cameras;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Death;
using SpecialHedgehog.Experience;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Framework.Physics;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Health;
using SpecialHedgehog.Hero;
using SpecialHedgehog.Input;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.Movement;
using SpecialHedgehog.Pause;
using SpecialHedgehog.PickUps;
using SpecialHedgehog.Projectiles;
using SpecialHedgehog.Time;
using SpecialHedgehog.Waves;
using UnityEngine;

namespace SpecialHedgehog.Framework
{
    
    public class EcsStartup : Singleton<EcsStartup>
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private SceneData sceneData;
        [SerializeField] private AudioService audioService;
        [SerializeField] private UIService uiService;

        private EcsWorld _mainWorld;
        private EcsWorld _eventWorld;
        private IEcsSystems _initSystems;
        private IEcsSystems _updateSystems;
        private IEcsSystems _fixedUpdateSystems;
        private IEcsSystems _lateUpdateSystems;

        private void Start()
        {
            _mainWorld = new EcsWorld();
            _eventWorld = new EcsWorld();
            
            var gameData = new GameData();
            var timeService = new TimeService();
            var inputService = new InputService();
            var inputMaster = new InputMaster();

            EcsPhysicsEvents.EcsWorld = _eventWorld;

            var shared = new object[]
            {
                gameData,
                sceneData,
                config,
                timeService,
                inputService,
                inputMaster,
                audioService,
                uiService
            };

            InitSystems(shared);
            UpdateSystems(shared);
            FixedUpdateSystems(shared);
            LateUpdateSystems(shared);
        }

        private void InitSystems(params object[] shared)
        {
            _initSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                
                .Add(new HeroSpawnSystem())
                
                .Add(new GemWalletInitSystem())
                
                .Add(new CameraInitSystem())
                ;

            _initSystems
                .Inject(shared)
                .Init();
        }

        private void UpdateSystems(params object[] shared)
        {
            _updateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
                .Add(new EcsWorldDebugSystem(Constants.Worlds.Events))
#endif
                .AddGroup(Constants.Features.Time, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new TimeSystem()})
                
                .Add(new GameInputSystem())
                .Add(new MenuInputSystem())
                
                    .DelHere<PauseEnabledEvent>(Constants.Worlds.Events)
                    .DelHere<PauseDisabledEvent>(Constants.Worlds.Events)
                .AddGroup(Constants.Features.Pause, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new PauseSystem()})
                    .DelHere<PauseRequest>(Constants.Worlds.Events)
                    .DelHere<DisablePauseRequest>(Constants.Worlds.Events)
                
                .Add(new InputToDirectionSystem())
                
                .AddGroup(Constants.Features.PistolAbility, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new PistolAbilitySystem()})
                
                .AddGroup(Constants.Features.ProjectileSpawn, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new ProjectileSpawnSystem()})
                    .DelHere<ProjectileSpawnRequest>(Constants.Worlds.Events)
                
                .AddGroup(Constants.Features.WaveSpawn, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new WaveSpawnSystem()})
                
                .AddGroup(Constants.Features.MobSpawn, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new MobSpawnSystem()})
                    .DelHere<MobSpawnRequest>(Constants.Worlds.Events)

                .AddGroup(Constants.Features.MobSpawn, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new MobDirectionUpdateSystem(),
                        new AttackCooldownReduceSystem(),
                        new MobAttackSystem()})
                    .DelHere<MobSpawnRequest>(Constants.Worlds.Events)
                    
                .AddGroup(Constants.Features.Timer, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new TimerUpdateSystem<AttackCooldown>()})
                
                .AddGroup(Constants.Features.Projectile, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new ProjectileEnemyHitSystem()})
                    .DelHere<FirstEnemyHit>()
                
                    .DelHere<Damaged>()
                    .DelHere<JustDied>()
                .AddGroup(Constants.Features.Damage, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new MakeDamageSystem()})
                    .DelHere<MakeDamageRequest>(Constants.Worlds.Events)
                
                .AddGroup(Constants.Features.PickUp, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                    new GemSpawnSystem(),
                    new GemPickUpSystem(),
                    new PickUpSoundSystem()})
                
                    .DelHere<ExperienceGained>()
                .AddGroup(Constants.Features.Experience, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new ExperiencePickUpSystem(),
                        new ExperienceGainSystem(),
                        new LevelUpSystem(),
                        new PlayerLevelUpSoundSystem()})
                    .DelHere<LevelUp>()
                
                .AddGroup(Constants.Features.Audio, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new HitSoundSystem(),
                        new DeathSoundSystem(),
                        new BackgroundMusicSystem()})
                
                .AddGroup(Constants.Features.Death, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new DeathSystem()})
                
                .AddGroup(Constants.Features.Movement, true, Constants.Worlds.Events,
                    new IEcsSystem [] {
                        new Rigidbody2DMovement()})
                
                ;

            _updateSystems
                .Inject(shared)
                .Init();
        }

        private void FixedUpdateSystems(params object[] shared)
        {
            _fixedUpdateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                
                ;

            _fixedUpdateSystems
                .Inject(shared)
                .Init();
        }

        private void LateUpdateSystems(params object[] shared)
        {
            _lateUpdateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                
                .Add(new HealthbarUIUpdateSystem())
                .Add(new WalletUIUpdateSystem())
                    .DelHere<ValueChanged>()
                .Add(new ExperienceUIUpdateSystem())
                ;

            _lateUpdateSystems
                .Inject(shared)
                .Init();
        }

#region Update Methods

        private void Update()
        {
            _updateSystems.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems.Run();
        }

        private void LateUpdate()
        {
            _lateUpdateSystems.Run();
        }
        
#endregion

        private void OnDestroy()
        {
            _initSystems.Destroy();
            _updateSystems.Destroy();
            _fixedUpdateSystems.Destroy();
            _lateUpdateSystems.Destroy();
            
            _eventWorld.Destroy();
            _mainWorld.Destroy();
        }

        public EcsWorld GetWorld(in string worldName) => _initSystems.GetWorld(worldName);
    }
}