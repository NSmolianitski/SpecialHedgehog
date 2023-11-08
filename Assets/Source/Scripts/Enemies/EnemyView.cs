using SpecialHedgehog.Scripts.Framework;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Enemies
{
    public class EnemyView : EntityOwner
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    }
}