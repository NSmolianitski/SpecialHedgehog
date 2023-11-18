using SpecialHedgehog.Experience;
using SpecialHedgehog.Framework;
using UnityEngine;

namespace SpecialHedgehog.UI
{
    public class PlayerExperienceScreen : UIScreen
    {
        [field: SerializeField] public ExperienceBarView ExperienceBar { get; private set; }
    }
}