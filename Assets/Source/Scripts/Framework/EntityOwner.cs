using Leopotam.EcsLite;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework
{
    public abstract class EntityOwner : MonoBehaviour
    {
        public EcsPackedEntity PackedEntity { get; set; }
    }
}