using System.Collections.Generic;
using ArtificeToolkit.Attributes;
using ArtificeToolkit.Runtime.SerializedDictionary;
using UnityEngine;
using ProjectEnums;

namespace Characters
{
    public class CharacterAdvancedStats : MonoBehaviour
    {
        /*private float increasedBasicStat;
        private float increasedHealth;
        private float increasedDamage;
        private float increasedArmour;
        private float increasedDodge;
        private float increasedMagicResistance;
        private float damageReduction;
        private float blockChance;
        private float blockDamageReduction;
        private float healthRegen;
        private float manaRegen;
        private float increasedPhysicalDamage;
        private float increasedMagicDamage;
        private float increasedSpellSkillDamage;
        private float increasedAttackSkillDamage;
        private float increasedAttackRange;
        private float increasedAttackSpeed;
        private float increasedCastSpeed;
        private float armourPenetration;
        private float magicDamagePenetration;
        private float additionalProjectiles;
        private float lifeSteal;
        private float bleedChance;
        private float burnChance;
        private float increasedBleedChance;
        private float increasedBurnChance;
        private float increasedSkillDuration;*/

        /*public float IncreasedBasicStat { get { return increasedBasicStat; } }
        public float IncreasedHealth { get { return increasedHealth; } }
        public float IncreasedDamage { get { return increasedDamage; } }
        public float IncreasedArmour { get { return increasedArmour; } }
        public float IncreasedMagicResistance { get { return increasedMagicResistance; } }
        public float IncreasedDodge { get { return increasedDodge; } }
        public float DamageReduction { get { return damageReduction; } }
        public float BlockChance { get { return blockChance; } }
        public float BlockDamageReduction { get { return blockDamageReduction; } }
        public float HealthRegen { get { return healthRegen; } }
        public float ManaRegen { get { return manaRegen; } }
        public float IncreasedPhysicalDamage { get { return increasedPhysicalDamage; } }
        public float IncreasedMagicDamage { get { return increasedMagicDamage; } }
        public float IncreasedSpellSkillDamage { get { return increasedSpellSkillDamage; } }
        public float IncreasedAttackSkillDamage { get { return increasedAttackSkillDamage; } }
        public float IncreasedAttackRange { get { return increasedAttackRange; } }
        public float IncreasedAttackSpeed { get { return increasedAttackSpeed; } }
        public float IncreasedCastSpeed { get { return increasedCastSpeed; } }
        public float ArmourPenetration { get { return armourPenetration; } }
        public float MagicDamagePenetration { get { return magicDamagePenetration; } }
        public float AdditionalProjectiles { get { return additionalProjectiles; } }
        public float LifeSteal { get { return lifeSteal; } }
        public float BleedChance { get { return bleedChance; } }
        public float BurnChance { get { return burnChance; } }
        public float IncreasedBleedChance { get { return increasedBleedChance; } }
        public float IncreasedBurnChance { get { return increasedBurnChance; } }
        public float IncreasedSkillDuration { get { return increasedSkillDuration; } }*/

        /*public void ModifyIncreasedBasicStat(EOperation op, float val) => Modify(ref increasedBasicStat, op, val);
        public void ModifyIncreasedHealth(EOperation op, float val) => Modify(ref increasedHealth, op, val);
        public void ModifyIncreasedDamage(EOperation op, float val) => Modify(ref increasedDamage, op, val);
        public void ModifyIncreasedArmour(EOperation op, float val) => Modify(ref increasedArmour, op, val);
        public void ModifyIncreasedMagicResistance(EOperation op, float val) => Modify(ref increasedMagicResistance, op, val);
        public void ModifyIncreasedDodge(EOperation op, float val) => Modify(ref increasedDodge, op, val);
        public void ModifyDamageReduction(EOperation op, float val) => Modify(ref damageReduction, op, val);
        public void ModifyBlockChance(EOperation op, float val) => Modify(ref blockChance, op, val);
        public void ModifyBlockDamageReduction(EOperation op, float val) => Modify(ref blockDamageReduction, op, val);
        public void ModifyHealthRegen(EOperation op, float val) => Modify(ref healthRegen, op, val);
        public void ModifyManaRegen(EOperation op, float val) => Modify(ref manaRegen, op, val);
        public void ModifyIncreasedPhysicalDamage(EOperation op, float val) => Modify(ref increasedPhysicalDamage, op, val);
        public void ModifyIncreasedMagicDamage(EOperation op, float val) => Modify(ref increasedMagicDamage, op, val);
        public void ModifyIncreasedSpellSkillDamage(EOperation op, float val) => Modify(ref increasedSpellSkillDamage, op, val);
        public void ModifyIncreasedAttackSkillDamage(EOperation op, float val) => Modify(ref increasedAttackSkillDamage, op, val);
        public void ModifyIncreasedAttackRange(EOperation op, float val) => Modify(ref increasedAttackRange, op, val);
        public void ModifyIncreasedAttackSpeed(EOperation op, float val) => Modify(ref increasedAttackSpeed, op, val);
        public void ModifyIncreasedCastSpeed(EOperation op, float val) => Modify(ref increasedCastSpeed, op, val);
        public void ModifyArmourPenetration(EOperation op, float val) => Modify(ref armourPenetration, op, val);
        public void ModifyMagicDamagePenetration(EOperation op, float val) => Modify(ref magicDamagePenetration, op, val);
        public void ModifyAdditionalProjectiles(EOperation op, float val) => Modify(ref additionalProjectiles, op, val);
        public void ModifyLifeSteal(EOperation op, float val) => Modify(ref lifeSteal, op, val);
        public void ModifyBleedChance(EOperation op, float val) => Modify(ref bleedChance, op, val);
        public void ModifyBurnChance(EOperation op, float val) => Modify(ref burnChance, op, val);
        public void ModifyIncreasedBleedChance(EOperation op, float val) => Modify(ref increasedBleedChance, op, val);
        public void ModifyIncreasedBurnChance(EOperation op, float val) => Modify(ref increasedBurnChance, op, val);
        public void ModifyIncreasedSkillDuration(EOperation op, float val) => Modify(ref increasedSkillDuration, op, val);*/

        [SerializeField, ReadOnly, ForceArtifice] private SerializedDictionary<EAdvancedStat, float> advancedStats = new()
        {
            { EAdvancedStat.INCREASED_STRENGTH, 0f },
            { EAdvancedStat.INCREASED_DEXTERITY, 0f },
            { EAdvancedStat.INCREASED_INTELLIGENCE, 0f },
            { EAdvancedStat.INCREASED_HEALTH, 0f },
            { EAdvancedStat.INCREASED_DAMAGE, 0f },
            { EAdvancedStat.INCREASED_ARMOUR, 0f },
            { EAdvancedStat.INCREASED_DODGE, 0f },
            { EAdvancedStat.INCREASED_MAGIC_RESISTANCE, 0f },
            { EAdvancedStat.DAMAGE_REDUCTION, 0f },
            { EAdvancedStat.BLOCK_CHANCE, 0f },
            { EAdvancedStat.BLOCK_DAMAGE_REDUCTION, 0f },
            { EAdvancedStat.HEALTH_REGEN, 0f },
            { EAdvancedStat.MANA_REGEN, 0f },
            { EAdvancedStat.INCREASED_PHYSICAL_DAMAGE, 0f },
            { EAdvancedStat.INCREASED_MAGIC_DAMAGE, 0f },
            { EAdvancedStat.INCREASED_SPELL_SKILL_DAMAGE, 0f },
            { EAdvancedStat.INCREASED_ATTACK_SKILL_DAMAGE, 0f },
            { EAdvancedStat.INCREASED_ATTACK_RANGE, 0f },
            { EAdvancedStat.INCREASED_ATTACK_SPEED, 0f },
            { EAdvancedStat.INCREASED_CAST_SPEED, 0f },
            { EAdvancedStat.ARMOUR_PENETRATION, 0f },
            { EAdvancedStat.MAGIC_DAMAGE_PENETRATION, 0f },
            { EAdvancedStat.ADDITIONAL_PROJECTILES, 0f },
            { EAdvancedStat.LIFE_STEAL, 0f },
            { EAdvancedStat.BLEED_CHANCE, 0f },
            { EAdvancedStat.BURN_CHANCE, 0f },
            { EAdvancedStat.INCREASED_BLEED_CHANCE, 0f },
            { EAdvancedStat.INCREASED_BURN_CHANCE, 0f },
            { EAdvancedStat.INCREASED_SKILL_DURATION, 0f }
        };

        public SerializedDictionary<EAdvancedStat, float> AdvancedStats => advancedStats;
        
        public void Modify(EAdvancedStat advancedStat, EOperation operation, float value)
        {
            if (operation == EOperation.SET) { advancedStats[advancedStat] = value; return; }
            if (operation == EOperation.SUBSTRACT) { value *= -1; }
            advancedStats[advancedStat] += value;
        }
        
        
    }
}
