using Leopotam.EcsLite;

namespace SpecialHedgehog.Scripts.Extensions
{
    public static class LeoEcsLiteExtensions
    {
        public static void TryAdd<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (!pool.Has(entity))
                pool.Add(entity);
        }
    }
}