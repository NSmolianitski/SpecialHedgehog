using UnityEngine;
using UnityEngine.UI;

namespace SpecialHedgehog.Experience
{
    public class ExperienceBarView : MonoBehaviour
    {
        [SerializeField] private Image experienceImage;

        public void UpdateExperience(float current, float max)
        {
            experienceImage.fillAmount = current / max;
        }
    }
}