using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;

namespace SpecialHedgehog.Time
{
    public class TimerUpdateSystem<TComponent> : IEcsRunSystem where TComponent : struct
    {
        private EcsFilterInject<Inc<Timer<TComponent>>> _filter;

        private EcsPoolInject<Timer<TComponent>> _timerPool;
        private EcsPoolInject<TimerFinished<TComponent>> _timerFinishedPool;

        private EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var timer = ref _timerPool.Value.Get(entity);
                timer.RemainingTime -= _timeService.Value.DeltaTime;

                if (timer.RemainingTime <= 0)
                    _timerFinishedPool.Value.Add(entity);
            }
        }
    }
}