using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace SpecialHedgehog.PickUps
{
    public class GemWalletInitSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<GemWalletOwner>> _filter;

        private EcsPoolInject<GemWalletOwner> _gemWalletOwnerPool;
        private EcsPoolInject<Wallet> _walletPool;
        private EcsPoolInject<Gem> _gemPool;
        private EcsPoolInject<WalletViewRef> _walletViewRefPool;
        private EcsPoolInject<ValueChanged> _valueChangedPool;

        private EcsWorldInject _world;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var gemWalletOwner = ref _gemWalletOwnerPool.Value.Get(entity);

                _walletPool.NewEntity(out var walletEntity);
                _gemPool.Value.Add(walletEntity);
                _valueChangedPool.Value.Add(walletEntity);
                
                ref var walletViewRef = ref _walletViewRefPool.Value.Add(walletEntity);
                walletViewRef.Value = Object.FindObjectOfType<WalletView>();
                
                gemWalletOwner.WalletPackedEntity = _world.Value.PackEntity(walletEntity);
            }
        }
    }
}