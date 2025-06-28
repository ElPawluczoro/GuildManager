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

        public float Health {  get { return health; }}
        public float CurrentHealth { get { return currentHealth; }}
        public float HealthRegeneration { get { return healthRegeneration; }}

        private void Start()
        {
            bStats = GetComponent<CharacterBasicStats>();
            aStats = GetComponent<CharacterAdvancedStats>();
        }

        public void UpdateMaxHealth()
        {
            //float currentHealthPortion = currentHealth / health; //implement if increasing max helth during battle become possible
            health = bStats.Vitality * 5; //TODO placeholder health calculating algorithm
            //currentHealth = health * currentHealthPortion;

        }

        public void UpdateHealthRegeneration()
        {
            healthRegeneration = aStats.HealthRegen;
        }

        public void RecieveDamage(DamageType damageType, float damage) //TODO placeholder damagereduction
        {
            float reduction = 0;
            if (damageType == DamageType.PHYSICAL)
            {
                reduction = bStats.Armour;
            }
            else
            {
                reduction = bStats.MagicResistance;
            }

            float actualDamage = damage - reduction;
            if (actualDamage <= 0) actualDamage = 1;
            currentHealth -= actualDamage;

            if (currentHealth <= 0) Die();
        }

        private void Die()
        {
            //TODO death logic
        }

        public void ReciveHealing(float amount)
        {
            currentHealth += amount;
            if(currentHealth > health) currentHealth = health;
        }

    }

    public enum DamageType
    {
        PHYSICAL, MAGICAL
    }
}
