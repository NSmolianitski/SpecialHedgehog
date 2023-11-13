using SpecialHedgehog.Health;
using StatefulUISupport.Scripts.Components;
using UnityEngine;

namespace SpecialHedgehog.UI
{
    public class PlayerHealthScreen : StatefulView
    {
        [field: SerializeField] public HealthbarView Healthbar { get; private set; }
    }
}