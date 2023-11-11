using BaboonAndCo.Extensions;
using Leopotam.EcsLite;
using SpecialHedgehog.Framework;
using SpecialHedgehog.Hero;
using UnityEngine;

namespace SpecialHedgehog.PickUps
{
    public class PickableView : EntityOwner
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.TryGetComponentInParent<HeroView>(out var heroView))
                return;
            
            OnPickableHeroTrigger(heroView);
        }

        private void OnPickableHeroTrigger(EntityOwner entityOwner)
        {
            var world = EcsStartup.Instance.GetWorld(null);
            var pool = world.GetPool<PickedUp>();

            PackedEntity.Unpack(world, out var pickableEntity);
            
            if (pool.Has(pickableEntity))
                return;
            
            ref var pickedUp = ref pool.Add(pickableEntity);
            pickedUp.PickerPackedEntity = entityOwner.PackedEntity;
        }
    }
}