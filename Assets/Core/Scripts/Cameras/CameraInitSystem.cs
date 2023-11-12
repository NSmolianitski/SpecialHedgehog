using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using SpecialHedgehog.Framework;

namespace SpecialHedgehog.Cameras
{
    public class CameraInitSystem : IEcsInitSystem
    {
        private EcsFilterInject<Inc<CameraTarget>> _cameraTargetFilter;

        private EcsPoolInject<CameraTarget> _cameraTargetPool;

        private EcsCustomInject<SceneData> _sceneData;
        
        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _cameraTargetFilter.Value)
            {
                ref var cameraTarget = ref _cameraTargetPool.Value.Get(entity);
                _sceneData.Value.MainVirtualCamera.Follow = cameraTarget.TargetTransform;
            }
        }
    }
}