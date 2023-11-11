using UnityEngine;
using UnityEngine.UI;

namespace SpecialHedgehog.Health
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