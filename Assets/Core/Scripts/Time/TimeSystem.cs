using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Services;

namespace SpecialHedgehog.Time
{
    public class TimeSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<TimeService> _ts;

        private EcsCustomInject<GameData> _gameData;
        
        public void Run(IEcsSystems systems)
        {
            if (_gameData.Value.PauseEnabled)
                return;
            
            _ts.Value.Time = UnityEngine.Time.time;
            _ts.Value.DeltaTime = UnityEngine.Time.deltaTime;
            _ts.Value.UnscaledTime = UnityEngine.Time.unscaledTime;
            _ts.Value.UnscaledDeltaTime = UnityEngine.Time.unscaledDeltaTime;
        }
    }
}