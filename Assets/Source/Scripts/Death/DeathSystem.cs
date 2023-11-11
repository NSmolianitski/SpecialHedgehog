using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Death
{
    public class DeathSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Dead, TransformRef>> _deadFilter;
        private EcsFilterInject<Inc<DieAfterTime>> _dieAfterTimeFilter;

        private EcsPoolInject<Dead> _deadPool;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<DieAfterTime> _dieAfterTimePool;

        private EcsCustomInject<TimeService> _timeService;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _dieAfterTimeFilter.Value)
            {
                ref var dieAfterTime = ref _dieAfterTimePool.Value.Get(entity);
                dieAfterTime.RemainingTime -= _timeService.Value.DeltaTime;

                if (dieAfterTime.RemainingTime <= 0)
                {
                    _dieAfterTimePool.Value.Del(entity);
                    _deadPool.Value.Add(entity);
                }
            }
            
            foreach (var entity in _deadFilter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                
                Object.Destroy(transformRef.Value.gameObject);
                _world.Value.DelEntity(entity);
            }
        }
    }
}