using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Death;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Framework.Configuration;
using SpecialHedgehog.Mobs;
using SpecialHedgehog.UnityRefs;
using UnityEngine;

namespace SpecialHedgehog.PickUps
{
    public class GemSpawnSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Mob, Dead, TransformRef>> _filter;

        private EcsPoolInject<TransformRef> _transformRefPool;
        private EcsPoolInject<Gem> _gemPool;
        private EcsPoolInject<Pickable> _pickablePool;
        private EcsPoolInject<Price> _pricePool;
        private EcsPoolInject<PickUpSounds> _pickUpSoundsPool;
        
        private EcsCustomInject<GameConfig> _gameConfig;
        private EcsCustomInject<SceneData> _sceneData;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var deadMobTransformRef = ref _transformRefPool.Value.Get(entity);
                
                var gemView = Object.Instantiate(_gameConfig.Value.GemPrefab, deadMobTransformRef.Value.position, 
                    Quaternion.identity, _sceneData.Value.GemParent);

                _gemPool.NewEntity(out var gemEntity);
                gemView.PackedEntity = _world.Value.PackEntity(gemEntity);

                ref var price = ref _pricePool.Value.Add(gemEntity);
                price.Value = _gameConfig.Value.GemPrice;

                ref var gemTransformRef = ref _transformRefPool.Value.Add(gemEntity);
                gemTransformRef.Value = gemView.transform;

                ref var pickUpSounds = ref _pickUpSoundsPool.Value.Add(gemEntity);
                pickUpSounds.AudioClips = _gameConfig.Value.GemPickUpSounds;
            }
        }
    }
}