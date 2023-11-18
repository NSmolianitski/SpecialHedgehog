using UnityEngine;

namespace SpecialHedgehog.Framework
{
    public abstract class UIScreen : MonoBehaviour
    {
        public void Open() => gameObject.SetActive(true);
        public void Close() => gameObject.SetActive(false);
    }
}