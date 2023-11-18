using SpecialHedgehog.Framework;
using SpecialHedgehog.PickUps;
using UnityEngine;

namespace SpecialHedgehog.UI
{
    public class PlayerWalletScreen : UIScreen
    {
        [field: SerializeField] public WalletView GemWallet { get; private set; }
    }
}