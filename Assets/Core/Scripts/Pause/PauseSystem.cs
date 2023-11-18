using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Services;

namespace SpecialHedgehog.Pause
{
    public class PauseSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<PauseRequest>> _pauseRequestFilter = Constants.Worlds.Events;
        private EcsFilterInject<Inc<DisablePauseRequest>> _disablePauseRequestFilter = Constants.Worlds.Events;

        private EcsPoolInject<PauseEnabledEvent> _pauseEnabledEventPool = Constants.Worlds.Events;
        private EcsPoolInject<PauseDisabledEvent> _pauseDisabledEventPool = Constants.Worlds.Events;

        private EcsFilterInject<Inc<EcsGroupSystemState>> _ecsGroupSystemStateFilter;
        private EcsPoolInject<EcsGroupSystemState> _ecsGroupSystemStatePool = Constants.Worlds.Events;
        
        private EcsCustomInject<GameData> _gameData;
        private EcsCustomInject<UIService> _uiService;
        private EcsCustomInject<TimeService> _timeService;
        
        private readonly List<string> _disableOnPauseGroupNames = new ();
        
        public void Init(IEcsSystems systems)
        {
            _disableOnPauseGroupNames.Add(Constants.Features.Time);
            _disableOnPauseGroupNames.Add(Constants.Features.Timer);
            _disableOnPauseGroupNames.Add(Constants.Features.Movement);
            _disableOnPauseGroupNames.Add(Constants.Features.WaveSpawn);
        }
        
        public void Run(IEcsSystems systems)
        {
            if (_disablePauseRequestFilter.Value.GetEntitiesCount() > 0)
                DisablePause();

            if (_pauseRequestFilter.Value.GetEntitiesCount() > 0)
                EnablePause();
        }

        private void EnablePause()
        {
            UnityEngine.Time.timeScale = 0;
            _timeService.Value.DeltaTime = 0;
            
            _pauseEnabledEventPool.NewEntity(out _);

            foreach (var groupName in _disableOnPauseGroupNames)
            {
                ref var ecsGroupSystemState = ref _ecsGroupSystemStatePool.NewEntity(out _);
                ecsGroupSystemState.Name = groupName;
                ecsGroupSystemState.State = false;
            }
        }

        private void DisablePause()
        {
            UnityEngine.Time.timeScale = 1;
            
            _pauseDisabledEventPool.NewEntity(out _);
            
            foreach (var groupName in _disableOnPauseGroupNames)
            {
                ref var ecsGroupSystemState = ref _ecsGroupSystemStatePool.NewEntity(out _);
                ecsGroupSystemState.Name = groupName;
                ecsGroupSystemState.State = true;
            }
            
            _uiService.Value.ClosePauseScreens();
        }
    }
}