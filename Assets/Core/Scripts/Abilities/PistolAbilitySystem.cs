﻿using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Audio;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.Projectiles;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.Abilities
{
    public class PistolAbilitySystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<PistolAbility, TransformRef>> _pistolAbilityFilter;
        private EcsFilterInject<Inc<Mob, TransformRef>> _mobFilter;

        private EcsPoolInject<ProjectileSpawnRequest> _projectileSpawnRequestPool = Constants.Worlds.Events;
        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<PistolAbility> _pistolAbilityPool;
        
        private EcsCustomInject<TimeService> _timeService;
        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<GameData> _gameData;
        private EcsCustomInject<AudioService> _audioService;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _pistolAbilityFilter.Value)
            {
                ref var ability = ref _pistolAbilityPool.Value.Get(entity);
                ability.Cooldown -= _timeService.Value.DeltaTime;
                if (ability.Cooldown > 0) continue;

                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                var position = transformRef.Value.position;
    
                if (_mobFilter.Value.GetEntitiesCount() is 0)
                    continue;
                var closestEnemy = GetClosestTargetToUnit(position);
                
                ref var projectileSpawnRequest = ref _projectileSpawnRequestPool.NewEntity(out _);
                projectileSpawnRequest.Damage = _gameConfig.Value.ProjectileDamage;
                projectileSpawnRequest.Speed = _gameConfig.Value.ProjectileSpeed;
                projectileSpawnRequest.Prefab = _gameConfig.Value.ProjectilePrefab;
                projectileSpawnRequest.SpawnPoint = position;
                projectileSpawnRequest.Direction = closestEnemy.transform.position - position;
                
                _audioService.Value.PlaySoundAtPoint(_gameConfig.Value.ShootSounds.GetRandom(), position); // TODO: change?

                ability.Cooldown = 1;
            }
        }
        
        private Transform GetClosestTargetToUnit(Vector3 unitPosition)
        {
            var rawEntities = _mobFilter.Value.GetRawEntities();
            
            Transform bestTarget = null;
            float closestDistanceSqr = Mathf.Infinity;
            
            for (var i = 0; i < _mobFilter.Value.GetEntitiesCount(); ++i)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(rawEntities[i]);
                var directionToTarget = transformRef.Value.position - unitPosition;
                
                var directionToTargetSqr = directionToTarget.sqrMagnitude;
                if (directionToTargetSqr < closestDistanceSqr)
                {
                    closestDistanceSqr = directionToTargetSqr;
                    bestTarget = transformRef.Value;
                }
            }
     
            return bestTarget;
        }
    }
}