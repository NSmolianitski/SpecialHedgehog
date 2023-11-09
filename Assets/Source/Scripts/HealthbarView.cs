using UnityEngine;
using UnityEngine.UI;

namespace SpecialHedgehog.Scripts
{
    public class HealthbarView : MonoBehaviour
    {
        [SerializeField] private Image health;

        public void UpdateHealth(float current, float max)
        {
            health.fillAmount = current / max;
        }
    }
}