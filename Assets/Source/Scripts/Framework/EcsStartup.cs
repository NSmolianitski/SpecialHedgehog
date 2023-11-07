using LeoEcsPhysics;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.Unity.Ugui;
using Leopotam.EcsLite.UnityEditor;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Framework.Services;
using SpecialHedgehog.Scripts.Time;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework
{
    public class EcsStartup : MonoBehaviour
    {
        [SerializeField] private ConfigurationSO config;
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
            // var inputMaster = new InputMaster();

            EcsPhysicsEvents.ecsWorld = _mainWorld;

            // Init
            _initSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                
                ;

            _initSystems
                // .ConvertScene()
                // .Inject(gameData, config, sceneData, inputMaster)
                .Init();

            // Update
            _updateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
                .Add(new EcsWorldDebugSystem(Constants.Worlds.Events))
#endif
                .Add(new TimeSystem())
                
                ;

            _updateSystems
                .InjectUgui(uiEmitter)
                // .Inject(inputService, inputMaster, timeService, gameData, config, sceneData)
                .Init();

            // Fixed Update
            _fixedUpdateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                ;

            _fixedUpdateSystems
                .Inject(inputService, timeService, gameData, config, sceneData)
                .Init();
            
            // Late Update
            _lateUpdateSystems = new EcsSystems(_mainWorld)
                .AddWorld(_eventWorld, Constants.Worlds.Events)
                ;

            _lateUpdateSystems
                .Inject(inputService, timeService, gameData, config, sceneData)
                .Init();
        }

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