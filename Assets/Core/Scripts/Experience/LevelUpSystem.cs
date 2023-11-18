using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Pause;
using SpecialHedgehog.UI;

namespace SpecialHedgehog.Experience
{
    public class LevelUpSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<Level, LevelUp, Experience>> _filter;

        private EcsPoolInject<LevelUp> _levelUpPool;
        private EcsPoolInject<Level> _levelPool;
        private EcsPoolInject<Experience> _experiencePool;
        private EcsPoolInject<PauseRequest> _pauseRequestPool = Constants.Worlds.Events;

        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<UIService> _uiService;

        private PlayerLevelUpScreen _playerLevelUpScreen;
        
        public void Init(IEcsSystems systems)
        {
            _playerLevelUpScreen = _uiService.Value.GetScreen<PlayerLevelUpScreen>();
            _playerLevelUpScreen.gameObject.SetActive(false);
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var experience = ref _experiencePool.Value.Get(entity);
                ref var level = ref _levelPool.Value.Get(entity);
                ++level.Current;
                
                experience.Current = 0;
                experience.TillNextLevel = level.LevelExperienceList[level.Current];

                _pauseRequestPool.NewEntity(out _);
                
                _playerLevelUpScreen.Open();
                _uiService.Value.AddPauseScreen(_playerLevelUpScreen);
            }
        }
    }
}