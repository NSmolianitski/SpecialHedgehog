using SpecialHedgehog.Framework;
using SpecialHedgehog.Health;
using UnityEngine;

namespace SpecialHedgehog.UI
{
    public class PlayerHealthScreen : UIScreen
    {
        [field: SerializeField] public HealthbarView Healthbar { get; private set; }
    }
}