using UnityEngine;

namespace Gameplay
{

    public abstract class ProgressBar : MonoBehaviour
    {
        [SerializeField] private GameObject healthBar;
        
        protected void UpdateProgressBar(float currentValue, float maxValue)
        {
            var percent = currentValue / maxValue;
            healthBar.transform.localScale = new Vector3(percent, 1, 1);
        }

    }
}