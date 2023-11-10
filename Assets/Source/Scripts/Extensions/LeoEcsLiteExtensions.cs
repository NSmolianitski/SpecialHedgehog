using Leopotam.EcsLite;

namespace SpecialHedgehog.Scripts.Extensions
{
    public static class LeoEcsLiteExtensions
    {
        public static bool TryAdd<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity)) return false;
            
            pool.Add(entity);
            return true;
        }
    }
}