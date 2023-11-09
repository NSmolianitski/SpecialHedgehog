using Leopotam.EcsLite;

namespace SpecialHedgehog.Scripts.Damage
{
    public struct MakeDamageRequest
    {
        public EcsPackedEntity Dealer;
        public EcsPackedEntity Target;
        public float Value;
    }
}