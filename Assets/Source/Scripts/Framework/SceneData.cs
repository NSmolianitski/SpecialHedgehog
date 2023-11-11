using Cinemachine;
using UnityEngine;

namespace SpecialHedgehog.Framework
{
    public class SceneData : MonoBehaviour
    {
        [field: Header("Cameras")]
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera MainVirtualCamera { get; private set; }
        
        [field: Header("Parents")]
        [field: SerializeField] public Transform MobParent { get; private set; }
        [field: SerializeField] public Transform ProjectileParent { get; private set; }
        [field: SerializeField] public Transform GemParent { get; private set; }
        
        [field: Header("Spawn Points")]
        [field: SerializeField] public Transform[] MobSpawnPoints { get; private set; }
        
    }
}