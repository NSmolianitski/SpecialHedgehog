using UnityEngine;

namespace SpecialHedgehog.Experience
{
    [CreateAssetMenu(fileName = "Player Levels Config", menuName = "Data/Player Levels Config")]
    public class PlayerLevelsConfig : ScriptableObject
    {
        [field: SerializeField] public float[] ExperiencePerLevel { get; private set; }
    }
}