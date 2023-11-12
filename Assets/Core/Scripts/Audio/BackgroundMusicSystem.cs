using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Configuration;

namespace SpecialHedgehog.Audio
{
    public class BackgroundMusicSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<AudioService> _audioService;
        
        public void Init(IEcsSystems systems)
        {
            _audioService.Value.PlayMusic(_gameConfig.Value.Music);
        }
        
        public void Run(IEcsSystems systems)
        {

        }
    }
}