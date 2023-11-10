using UnityEngine;

namespace SpecialHedgehog.Scripts.Projectiles
{
    public struct ProjectileSpawnRequest
    {
        public Vector3 SpawnPoint;
        public float Damage;
        public float Speed;
        public ProjectileView Prefab;
        public Vector2 Direction;
    }
}