using BaboonAndCo.Utils;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Framework.Configuration;
using SpecialHedgehog.Scripts.Framework.Services;
using SpecialHedgehog.Scripts.Mobs;
using SpecialHedgehog.Scripts.Projectiles;
using SpecialHedgehog.Scripts.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Abilities
{
    public class PistolAbilitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PistolAbility, TransformRef>> _filter;

        private EcsPoolInject<ProjectileSpawnRequest> _projectileSpawnRequestPool = Constants.Worlds.Events;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<PistolAbility> _pistolAbilityPool;
        
        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<GameConfig> _gameConfig;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var ability = ref _pistolAbilityPool.Value.Get(entity);
                ability.Cooldown -= _timeService.Value.DeltaTime;
                if (ability.Cooldown > 0) continue;

                var mobs = Object.FindObjectsOfType<MobView>();
                if (mobs.Length == 0)
                    return;
                
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                var closestEnemy = TransformUtils.GetClosestTargetToUnit(mobs, transformRef.Value);
                
                ref var projectileSpawnRequest = ref _projectileSpawnRequestPool.NewEntity(out _);
                projectileSpawnRequest.Damage = _gameConfig.Value.ProjectileDamage;
                projectileSpawnRequest.Speed = _gameConfig.Value.ProjectileSpeed;
                projectileSpawnRequest.Prefab = _gameConfig.Value.ProjectilePrefab;
                projectileSpawnRequest.SpawnPoint = transformRef.Value.position;
                projectileSpawnRequest.Direction = closestEnemy.transform.position - transformRef.Value.position;

                ability.Cooldown = 1;
            }
        }
    }
}