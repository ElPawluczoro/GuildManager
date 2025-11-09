using System;
using Characters;
using UnityEngine;

namespace Gameplay.Combat
{
    public class HealthBar : ProgressBar
    {
        [SerializeField] private CharacterHealth characterHealth;

        private void OnEnable()
        {
            characterHealth.onHealthChanged += UpdateProgressBar;
        }

        private void OnDisable()
        {
            characterHealth.onHealthChanged -= UpdateProgressBar;
        }
        
        
    }
}