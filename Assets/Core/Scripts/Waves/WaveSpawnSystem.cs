using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Services;
using SpecialHedgehog.Mobs;
using UnityEngine;

namespace SpecialHedgehog.Waves
{
    public class WaveSpawnSystem : IEcsRunSystem
    {
        private EcsPoolInject<MobSpawnRequest> _mobSpawnRequestPool = Constants.Worlds.Events;
        
        private const float Cooldown = 0.3f;
        private float _timer;

        private EcsCustomInject<TimeService> _timeService;
        
        public void Run(IEcsSystems systems)
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
                SpawnWave();
            
            _timer -= _timeService.Value.DeltaTime;

            if (_timer <= 0)
            {
                SpawnWave();
                _timer = Cooldown;
            }
        }

        private void SpawnWave()
        {
            // TODO: Add Wave Spawn Logic
            
            _mobSpawnRequestPool.NewEntity(out _);
        }
    }
}