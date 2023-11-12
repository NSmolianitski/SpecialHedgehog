using Leopotam.EcsLite;
using UnityEngine;

namespace SpecialHedgehog.Framework
{
    public abstract class EntityOwner : MonoBehaviour
    {
        public EcsPackedEntity PackedEntity { get; set; }
    }
}