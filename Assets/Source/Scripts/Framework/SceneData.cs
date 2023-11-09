using Cinemachine;
using UnityEngine;

namespace SpecialHedgehog.Scripts.Framework
{
    public class SceneData : MonoBehaviour
    {
        [field: SerializeField] public Camera MainCamera { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera MainVirtualCamera { get; private set; }
        [field: SerializeField] public Transform MobParent { get; private set; }
        [field: SerializeField] public Transform ProjectileParent { get; private set; }
        [field: SerializeField] public Transform[] MobSpawnPoints { get; private set; }
    }
}