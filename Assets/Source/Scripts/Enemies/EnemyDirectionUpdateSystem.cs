using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Movement;
using SpecialHedgehog.Scripts.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Enemies
{
    public class EnemyDirectionUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<EnemyTarget>> _enemyTargetFilter;
        private EcsFilterInject<Inc<Enemy, Direction, TransformRef>> _enemyFilter;

        private EcsPoolInject<EnemyTarget> _enemyTargetPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<TransformRef> _transformRefPool;

        public void Run(IEcsSystems systems)
        {
            Transform target = null;
            foreach (var entity in _enemyTargetFilter.Value)
            {
                ref var enemyTarget = ref _enemyTargetPool.Value.Get(entity);
                target = enemyTarget.TargetTransform;
            }
            
            foreach (var entity in _enemyFilter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                ref var direction = ref _directionPool.Value.Get(entity);
                direction.Value = target.position - transformRef.Value.position;
            }
        }
    }
}