using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Death;

namespace SpecialHedgehog.PickUps
{
    public class GemPickUpSystem : IEcsRunSystem
    {
        private EcsFilterInject<Inc<Gem, PickedUp, Price>> _filter;

        private EcsPoolInject<PickedUp> _pickedUpPool;
        private EcsPoolInject<GemWalletOwner> _gemWalletOwnerPool;
        private EcsPoolInject<Price> _pricePool;
        private EcsPoolInject<Wallet> _walletPool;
        private EcsPoolInject<ValueChanged> _valueChangedPool;
        private EcsPoolInject<Dead> _dead;

        private EcsWorldInject _world;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var pickedUp = ref _pickedUpPool.Value.Get(entity);
                pickedUp.PickerPackedEntity.Unpack(_world.Value, out var pickerEntity);

                ref var gemWalletOwner = ref _gemWalletOwnerPool.Value.Get(pickerEntity);
                gemWalletOwner.WalletPackedEntity.Unpack(_world.Value, out var gemWalletEntity);

                ref var price = ref _pricePool.Value.Get(entity);
                
                ref var wallet = ref _walletPool.Value.Get(gemWalletEntity);
                wallet.CurrentValue += price.Value;
                _valueChangedPool.Value.Add(gemWalletEntity);
                
                _dead.Value.Add(entity);
            }
        }
    }
}