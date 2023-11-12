using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Movement;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Mobs
{
    public class MobDirectionUpdateSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<MobTarget>> _enemyTargetFilter;
        private EcsFilterInject<Inc<Mob, Direction, TransformRef>> _enemyFilter;

        private EcsPoolInject<MobTarget> _enemyTargetPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<TransformRef> _transformRefPool;

        public void Run(IEcsSystems systems)
        {
            Transform target = null;
            foreach (var entity in _enemyTargetFilter.Value)
            {
                ref var mobTarget = ref _enemyTargetPool.Value.Get(entity);
                target = mobTarget.TargetTransform;
            }
            
            foreach (var entity in _enemyFilter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                ref var direction = ref _directionPool.Value.Get(entity);
                
                var targetVector = (Vector2) (target.position - transformRef.Value.position);
                if (targetVector.sqrMagnitude < 0.01f)
                    targetVector = Vector3.zero;
                
                direction.Value = targetVector;
            }
        }
    }
}