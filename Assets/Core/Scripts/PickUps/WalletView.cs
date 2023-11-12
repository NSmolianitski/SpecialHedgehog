using TMPro;
using UnityEngine;

namespace SpecialHedgehog.PickUps
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI valueText;

        public void UpdateText(float value) => valueText.text = $"{value}";
    }
}