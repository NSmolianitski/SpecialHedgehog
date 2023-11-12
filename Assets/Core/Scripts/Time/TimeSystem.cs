using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;

namespace SpecialHedgehog.Time
{
    public class TimeSystem : IEcsRunSystem
    {
        private readonly EcsCustomInject<TimeService> _ts;

        public void Run(IEcsSystems systems)
        {
            _ts.Value.Time = UnityEngine.Time.time;
            _ts.Value.DeltaTime = UnityEngine.Time.deltaTime;
            _ts.Value.UnscaledTime = UnityEngine.Time.unscaledTime;
            _ts.Value.UnscaledDeltaTime = UnityEngine.Time.unscaledDeltaTime;
        }
    }
}