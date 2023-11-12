using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace SpecialHedgehog.PickUps
{
    public class WalletUIUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilterInject<Inc<WalletViewRef, Gem, Wallet, ValueChanged>> _filter;

        private EcsPoolInject<WalletViewRef> _walletRefPool;
        private EcsPoolInject<Wallet> _walletPool;
        
        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                UpdateWalletUI(entity);
            }
        }
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                UpdateWalletUI(entity);
            }
        }

        private void UpdateWalletUI(int walletEntity)
        {
            ref var wallet = ref _walletPool.Value.Get(walletEntity);
            ref var walletViewRef = ref _walletRefPool.Value.Get(walletEntity);
            walletViewRef.Value.UpdateText(wallet.CurrentValue);
        }
    }
}