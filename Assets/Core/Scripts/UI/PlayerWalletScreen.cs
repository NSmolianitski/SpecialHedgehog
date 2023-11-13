using SpecialHedgehog.PickUps;
using StatefulUISupport.Scripts.Components;
using UnityEngine;

namespace SpecialHedgehog.UI
{
    public class PlayerWalletScreen : StatefulView
    {
        [field: SerializeField] public WalletView GemWallet { get; private set; }
    }
}