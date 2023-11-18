using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Audio;
using SpecialHedgehog.Framework.Configuration;

namespace SpecialHedgehog.Experience
{
    public class PlayerLevelUpSoundSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Hero.Hero, LevelUp>> _filter;

        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<AudioService> _audioService;
        
        public void Run(IEcsSystems systems)
        {
            if (_filter.Value.GetEntitiesCount() == 0)
                return;
            
            _audioService.Value.PlaySound(_gameConfig.Value.LevelUpSound);
        }
    }
}