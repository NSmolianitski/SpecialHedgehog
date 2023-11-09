using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SpecialHedgehog.Scripts.Death
{
    public class DeathSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Dead>> _deadFilter;

        private EcsPoolInject<Dead> _deadPool;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _deadFilter.Value)
            {
                
            }
        }
    }
}