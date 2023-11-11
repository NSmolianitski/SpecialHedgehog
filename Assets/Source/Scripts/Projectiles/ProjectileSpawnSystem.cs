using BaboonAndCo.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Damage;
using SpecialHedgehog.Death;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Movement;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Projectiles
{
    public class ProjectileSpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<ProjectileSpawnRequest>> _filter = Constants.Worlds.Events;

        private EcsPoolInject<ProjectileSpawnRequest> _spawnProjectileRequestPool = Constants.Worlds.Events;
        
        private EcsPoolInject<Projectile> _projectilePool;
        private EcsPoolInject<DamageStat> _damageStatPool;
        private EcsPoolInject<Direction> _directionPool;
        private EcsPoolInject<Rigidbody2DRef> _rigidbody2DPool;
        private EcsPoolInject<DieAfterTime> _dieAfterTimePool;
        private EcsPoolInject<TransformRef> _transformRefPool;

        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var spawnRequest = ref _spawnProjectileRequestPool.Value.Get(entity);

                _projectilePool.NewEntity(out var projectileEntity);
                
                var projectileView = Object.Instantiate(spawnRequest.Prefab, spawnRequest.SpawnPoint,
                    Quaternion.identity, _sceneData.Value.ProjectileParent);
                projectileView.PackedEntity = _world.Value.PackEntity(projectileEntity);

                ref var damageStat = ref _damageStatPool.Value.Add(projectileEntity);
                damageStat.InitValue = spawnRequest.Damage;
                damageStat.CurrentValue = damageStat.InitValue;
                
                ref var rigidbody2DRef = ref _rigidbody2DPool.Value.Add(projectileEntity);
                rigidbody2DRef.Value = projectileView.Rigidbody2D;

                projectileView.Rigidbody2D.velocity = spawnRequest.Direction.normalized * spawnRequest.Speed;

                ref var dieAfterTime = ref _dieAfterTimePool.Value.Add(projectileEntity);
                dieAfterTime.RemainingTime = 4;

                ref var transformRef = ref _transformRefPool.Value.Add(projectileEntity);
                transformRef.Value = projectileView.transform;
                
                TransformUtils.RotateByDirection(transformRef.Value, spawnRequest.Direction);
            }
        }
    }
}