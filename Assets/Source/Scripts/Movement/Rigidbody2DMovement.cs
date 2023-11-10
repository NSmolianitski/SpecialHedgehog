using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Framework.Services;
using SpecialHedgehog.Scripts.UnityRefs;

namespace SpecialHedgehog.Scripts.Movement
{
    public class Rigidbody2DMovement : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Rigidbody2DRef, Speed, Direction>> _filter;

        private EcsPoolInject<Rigidbody2DRef> _rigidbody2DPool;
        private EcsPoolInject<Speed> _speedPool;
        private EcsPoolInject<Direction> _directionPool;

        private EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var rigidbody2DRef = ref _rigidbody2DPool.Value.Get(entity);
                ref var speed = ref _speedPool.Value.Get(entity);
                ref var direction = ref _directionPool.Value.Get(entity);

                var moveVector = direction.Value.normalized * speed.Value;
                rigidbody2DRef.Value.velocity = moveVector;
            }
        }
    }
}