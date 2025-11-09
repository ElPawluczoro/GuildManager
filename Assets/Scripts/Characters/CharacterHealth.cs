using Gameplay.Combat;
using ProjectEnums;
using UnityEngine;

namespace Characters
{
    public class CharacterHealth : MonoBehaviour
    {
        [SerializeField] private float health;
        [SerializeField] private float currentHealth;
        [SerializeField] private float healthRegeneration;

        private CharacterBasicStats bStats;
        private CharacterAdvancedStats aStats;
        
        public delegate void OnHealthChanged(float currentHealth, float maxHealth);
        public event OnHealthChanged onHealthChanged;
        
        public float Health {  get { return health; }}
        public float CurrentHealth { get { return currentHealth; }}
        public float HealthRegeneration { get { return healthRegeneration; }}

        private void Start()
        {
            SetUpDependences();
        }

        public void SetUpDependences()
        {
            bStats = GetComponent<CharacterBasicStats>();
            aStats = GetComponent<CharacterAdvancedStats>();
        }

        public void UpdateMaxHealth()
        {
            //float currentHealthPortion = currentHealth / health; //implement if increasing max health during battle become possible
            health = (float)bStats.BasicStats[ECharacterBasicStat.VITALITY] * 5; //TODO placeholder health calculating algorithm
            currentHealth = health;
            //currentHealth = health * currentHealthPortion;

        }

        public void UpdateHealthRegeneration()
        {
            healthRegeneration = aStats.AdvancedStats[EAdvancedStat.HEALTH_REGEN];
        }

        public void ReceiveDamage(DamageType damageType, float damage) //TODO placeholder damage reduction
        {
            float reduction = 0;
            if (damageType == DamageType.PHYSICAL)
            {
                reduction = bStats.BasicStats[ECharacterBasicStat.ARMOUR];
            }
            else
            {
                reduction = bStats.BasicStats[ECharacterBasicStat.MAGIC_RESISTANCE];
            }

            float actualDamage = damage - reduction;
            if (actualDamage <= 0) actualDamage = 1;
            currentHealth -= actualDamage;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                Die();
            }

            if (onHealthChanged != null)
            {
                onHealthChanged.Invoke(currentHealth, health);
            }
        }

        private void Die()
        {
            //TODO death logic
            GetComponent<FightingScript>().alive = false;
            GetComponent<FollowTarget>().enabled = false;
            transform.GetChild(0).gameObject.SetActive(false); // Canvas
            FindFirstObjectByType<CombatController>().CheckResult();
        }

        public void ReceiveHealing(float amount)
        {
            currentHealth += amount;
            if(currentHealth > health) currentHealth = health;
        }

        public bool IsDead()
        {
            return currentHealth <= 0;
        }
    }

    public enum DamageType
    {
        PHYSICAL, MAGICAL
    }
}
