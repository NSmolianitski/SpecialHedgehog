using System.Collections.Generic;
using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite.UnityEditor;
using SpecialHedgehog.Scripts.Cameras;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Framework.Services;
using SpecialHedgehog.Scripts.Hero;
using SpecialHedgehog.Scripts.Input;
using SpecialHedgehog.Scripts.Movement;
using SpecialHedgehog.Scripts.Time;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private GameConfig config;
        [SerializeField] private SceneData sceneData;
        [SerializeField] private EcsUguiEmitter uiEmitter;

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

            EcsPhysicsEvents.ecsWorld = _mainWorld;

            var shared = new object[]
            {
                gameData,
                sceneData,
                config,
                timeService,
                inputService,
                inputMaster
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
                
                .Add(new Rigidbody2DMovement())
                ;

            _updateSystems
                .InjectUgui(uiEmitter)
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
            
            _eventWorld.Destroy();
            _mainWorld.Destroy();
        }
    }
}