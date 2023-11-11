using Leopotam.EcsLite;

namespace SpecialHedgehog.Damage
{
    public struct MakeDamageRequest
    {
        public EcsPackedEntity Dealer;
        public EcsPackedEntity Target;
        public float Value;
    }
}