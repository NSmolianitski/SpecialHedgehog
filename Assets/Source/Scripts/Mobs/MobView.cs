using SpecialHedgehog.Scripts.Damage;
using SpecialHedgehog.Scripts.Framework;
using SpecialHedgehog.Scripts.Hero;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Mobs
{
    public class MobView : EntityOwner
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    }
}