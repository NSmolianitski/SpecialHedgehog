using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Input;

namespace SpecialHedgehog.Movement
{
    public class InputToDirectionSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<InputListener, Direction>> _filter;

        private EcsPoolInject<Direction> _directionPool;
        
        private EcsCustomInject<InputService> _inputService;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var direction = ref _directionPool.Value.Get(entity);
                direction.Value = _inputService.Value.MovementDirection;
            }
        }
    }
}