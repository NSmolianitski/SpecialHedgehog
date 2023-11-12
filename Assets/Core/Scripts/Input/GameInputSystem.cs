using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework.Services;
using UnityEngine;

namespace SpecialHedgehog.Input
{
    public class GameInputSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsCustomInject<InputService> _inputService;
        private EcsCustomInject<InputMaster> _inputMaster;

        private InputMaster.HeroActions _heroActions; 
        
        public void Init(IEcsSystems systems)
        {
            _heroActions = _inputMaster.Value.Hero;
            _heroActions.Enable();
        }
        
        public void Run(IEcsSystems systems)
        {
            _inputService.Value.MovementDirection = _heroActions.Movement.ReadValue<Vector2>();
        }
    }
}