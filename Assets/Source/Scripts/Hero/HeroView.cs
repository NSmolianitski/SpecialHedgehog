﻿using SpecialHedgehog.Scripts.Framework;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Hero
{
    public class HeroView : EntityOwner
    {
        [field: SerializeField] public SpriteRenderer SpriteRenderer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody2D { get; private set; }
    }
}