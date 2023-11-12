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
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Framework.Physics;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Health;
using SpecialHedgehog.Hero;
using SpecialHedgehog.Input;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.Movement;
using SpecialHedgehog.PickUps;
using SpecialHedgehog.Projectiles;
using SpecialHedgehog.Time;
using UnityEngine;

namespace SpecialHedgehog.Framework
{
    public class EcsStartup : Singleton<EcsStartup>
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private SceneData sceneData;
        [SerializeField] private AudioService audioService;

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
                audioService
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
                .Add(new TimeSystem())
                
                .Add(new GameInputSystem())
                .Add(new MenuInputSystem())
                
                .Add(new InputToDirectionSystem())
                
                .Add(new PistolAbilitySystem())
                
                .Add(new ProjectileSpawnSystem())
                    .DelHere<ProjectileSpawnRequest>(Constants.Worlds.Events)
                
                .Add(new MobSpawnSystem())
                .Add(new MobDirectionUpdateSystem())
                .Add(new AttackCooldownReduceSystem())
                .Add(new MobAttackSystem())
                
                .Add(new ProjectileEnemyHitSystem())
                    .DelHere<FirstEnemyHit>()
                
                    .DelHere<Damaged>()
                    .DelHere<JustDied>()
                .Add(new MakeDamageSystem())
                    .DelHere<MakeDamageRequest>(Constants.Worlds.Events)
                
                .Add(new GemSpawnSystem())
                .Add(new GemPickUpSystem())
                    .DelHere<PickedUp>()
                
                .Add(new DeathAudioSystem())
                .Add(new DeathSystem())
                
                .Add(new Rigidbody2DMovement())
                
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