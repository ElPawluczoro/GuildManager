using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace Characters.Player.CharacterItem
{
    public class AffixGroup
    {
        public Dictionary<Affixes, Vector2Int[]> Prefixes = new();
        public Dictionary<Affixes, Vector2Int[]> Suffixes = new();
    }
    
    public class AffixGenerator: MonoBehaviour
    {
        public static Dictionary<Affixes, Vector2Int[]> prefixes = new(); //3 tiers
        public static Dictionary<Affixes, Vector2Int[]> suffixes = new(); //3 tiers
        
        public static Dictionary<EquipmentType, AffixGroup> affixGroups = new(); 
        
        //prefixes
        private Vector2Int[] flatStatRanges = { new(2, 5), new(6, 10), new(11, 20) };
        private Vector2Int[] maxHealthRanges = { new(10, 15), new(16, 25), new(26, 40) };
        private Vector2Int[] healthRegenRanges = { new(2, 4), new(5, 8), new(9, 20) };
        private Vector2Int[] manaRegenRanges = { new(1, 2), new(3, 4), new(5, 7) };
        private Vector2Int[] increasedDamageRanges = { new(10, 15), new(16, 25), new(26, 50) };
        private Vector2Int[] increasedSkillDamageRanges = { new(5, 10), new(11, 20), new(21, 40) };
        private Vector2Int[] increasedRangeRanges = { new(5, 10), new(11, 20), new(21, 40) };
        private Vector2Int[] increasedAttackSpeedRanges = { new(5, 10), new(11, 20), new(21, 40) };
        private Vector2Int[] increasedCastSpeedRanges = { new(5, 10), new(11, 20), new(21, 40) };
        private Vector2Int[] lifeStealRanges = { new(1, 2), new(3, 5), new(6, 10) };
        private Vector2Int[] penetrationRanges = { new(5, 7), new(8, 12), new(13, 20) };
        private Vector2Int[] increasedSkillDurationRanges = { new(5, 10), new(11, 20), new(21, 40) };

        //suffixes
        private Vector2Int[] defensivesRanges = { new(10, 15), new(16, 25), new(26, 40) };
        private Vector2Int[] increasedGlobalDamageRanges = { new(3, 7), new(8, 15), new(16, 30) };
        private Vector2Int[] increasedDefensivesLocal = { new(100, 200), new(201, 300), new(301, 400) };
        private Vector2Int[] damageReductionRanges = { new(1, 2), new(3, 5), new(6, 9) };
        private Vector2Int[] blockChanceRanges = { new(1, 2), new(3, 4), new(5, 7) };
        private Vector2Int[] blockDamageReductionRanges = { new(3, 5), new(6, 9), new(10, 15) };
        private Vector2Int[] ailmentChance = { new(3, 5), new(6, 9), new(10, 15) };
        private Vector2Int[] increasedAilmentChance = { new(5, 10), new(11, 25), new(26, 50) };
        
        private void Awake()
        {
            if (prefixes.Count > 0) return;
            InitializeAffixesLists();

            InitializeAffixesGroups();
        }

        private void InitializeAffixesGroups()
        {
            Affixes[] helmetAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.HEALTH_REGEN, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, Affixes.INCREASED_DODGE_LOCAL
            };
            InitializeAffixGroup(EquipmentType.HELMET, helmetAffixes);
            
            Affixes[] bodyArmourPrefixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.HEALTH_REGEN, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, Affixes.INCREASED_DODGE_LOCAL, 
                Affixes.DAMAGE_REDUCTION
            };
            InitializeAffixGroup(EquipmentType.BODY_ARMOUR, bodyArmourPrefixes);
            
            Affixes[] bootsPrefixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, Affixes.INCREASED_DODGE_LOCAL, 
                Affixes.DAMAGE_REDUCTION
            };
            InitializeAffixGroup(EquipmentType.BOOTS, bootsPrefixes);
            
            Affixes[] glovesPrefixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, Affixes.INCREASED_DODGE_LOCAL,
                Affixes.INCREASED_CAST_SPEED, Affixes.INCREASED_ATTACK_SPEED, 
                Affixes.INCREASED_ATTACK_SKILL_DAMAGE, Affixes.INCREASED_SPELL_SKILL_DAMAGE
            };
            InitializeAffixGroup(EquipmentType.GLOVES, glovesPrefixes);

            Affixes[] ringAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.HEALTH_REGEN, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.BLEED_CHANCE, Affixes.BURN_CHANCE, Affixes.INCREASED_ARMOUR_GLOBAL,
                Affixes.INCREASED_MAGIC_RESISTANCE_GLOBAL, Affixes.INCREASED_DODGE_GLOBAL
            };
            InitializeAffixGroup(EquipmentType.RING, ringAffixes);

            Affixes[] amuletAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.HEALTH_REGEN, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.BLEED_CHANCE, Affixes.BURN_CHANCE, Affixes.BLOCK_CHANCE, Affixes.BLOCK_DAMAGE_REDUCTION
            };
            InitializeAffixGroup(EquipmentType.AMULET, amuletAffixes);

            Affixes[] weaponAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE, Affixes.BLEED_CHANCE,
                Affixes.INCREASED_BLEED_CHANCE, Affixes.INCREASED_ATTACK_SPEED, Affixes.INCREASED_PHYSICAL_DAMAGE,
                Affixes.ARMOUR_PENETRATION, Affixes.INCREASED_DAMAGE, Affixes.INCREASED_ATTACK_SKILL_DAMAGE,
            };
            InitializeAffixGroup(EquipmentType.TWO_HANDED, weaponAffixes);
            InitializeAffixGroup(EquipmentType.MAIN_HAND_WEAPON, weaponAffixes);
            InitializeAffixGroup(EquipmentType.OFF_HAND_WEAPON, weaponAffixes);

            Affixes[] bowAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE, Affixes.BLEED_CHANCE,
                Affixes.INCREASED_BLEED_CHANCE, Affixes.INCREASED_ATTACK_SPEED, Affixes.INCREASED_PHYSICAL_DAMAGE,
                Affixes.ARMOUR_PENETRATION, Affixes.INCREASED_DAMAGE, Affixes.INCREASED_ATTACK_SKILL_DAMAGE,
                Affixes.INCREASED_ATTACK_RANGE
            };
            InitializeAffixGroup(EquipmentType.BOW, bowAffixes);
            
            Affixes[] magicWeaponAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE, Affixes.BURN_CHANCE, 
                Affixes.INCREASED_BURN_CHANCE, Affixes.INCREASED_MAGIC_DAMAGE, Affixes.MAGIC_DAMAGE_PENETRATION, 
                Affixes.INCREASED_DAMAGE, Affixes.INCREASED_SPELL_SKILL_DAMAGE
            };
            InitializeAffixGroup(EquipmentType.MAIN_HAND_MAGIC_WEAPON, magicWeaponAffixes);

            Affixes[] shieldAffixes =
            {
                Affixes.FLAT_STRENGTH, Affixes.FLAT_DEXTERITY, Affixes.FLAT_INTELLIGENCE,
                Affixes.FLAT_VITALITY, Affixes.HEALTH_REGEN, Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE,
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, Affixes.INCREASED_DODGE_LOCAL,
                Affixes.DAMAGE_REDUCTION, Affixes.BLOCK_CHANCE, Affixes.BLOCK_DAMAGE_REDUCTION
            };
            InitializeAffixGroup(EquipmentType.OFF_HAND_SHIELD, shieldAffixes);
        }

        private void InitializeAffixesLists()
        {
            prefixes.Add(Affixes.FLAT_STRENGTH, flatStatRanges);
            prefixes.Add(Affixes.FLAT_DEXTERITY, flatStatRanges);
            prefixes.Add(Affixes.FLAT_INTELLIGENCE, flatStatRanges);
            prefixes.Add(Affixes.FLAT_VITALITY, maxHealthRanges);
            prefixes.Add(Affixes.HEALTH_REGEN, healthRegenRanges);
            prefixes.Add(Affixes.MANA_REGEN, manaRegenRanges);
            prefixes.Add(Affixes.INCREASED_PHYSICAL_DAMAGE, increasedDamageRanges);
            prefixes.Add(Affixes.INCREASED_MAGIC_DAMAGE, increasedDamageRanges);
            prefixes.Add(Affixes.INCREASED_SPELL_SKILL_DAMAGE, increasedSkillDamageRanges);
            prefixes.Add(Affixes.INCREASED_ATTACK_SKILL_DAMAGE, increasedSkillDamageRanges);
            prefixes.Add(Affixes.INCREASED_ATTACK_RANGE, increasedRangeRanges);
            prefixes.Add(Affixes.INCREASED_ATTACK_SPEED, increasedAttackSpeedRanges);
            prefixes.Add(Affixes.INCREASED_CAST_SPEED, increasedCastSpeedRanges);
            prefixes.Add(Affixes.LIFE_STEAL, lifeStealRanges);
            prefixes.Add(Affixes.ARMOUR_PENETRATION, penetrationRanges);
            prefixes.Add(Affixes.MAGIC_DAMAGE_PENETRATION, penetrationRanges);
            prefixes.Add(Affixes.INCREASED_SKILL_DURATION, increasedSkillDurationRanges);
            
            suffixes.Add(Affixes.ARMOUR, defensivesRanges);
            suffixes.Add(Affixes.MAGIC_RESISTANCE, defensivesRanges);
            suffixes.Add(Affixes.DODGE, defensivesRanges);
            suffixes.Add(Affixes.INCREASED_DAMAGE, increasedGlobalDamageRanges);
            suffixes.Add(Affixes.INCREASED_ARMOUR_LOCAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.INCREASED_DODGE_LOCAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.INCREASED_ARMOUR_GLOBAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.INCREASED_MAGIC_RESISTANCE_GLOBAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.INCREASED_DODGE_GLOBAL, increasedDefensivesLocal);
            suffixes.Add(Affixes.DAMAGE_REDUCTION, damageReductionRanges);
            suffixes.Add(Affixes.BLOCK_CHANCE, blockChanceRanges);
            suffixes.Add(Affixes.BLOCK_DAMAGE_REDUCTION, blockDamageReductionRanges);
            suffixes.Add(Affixes.BLEED_CHANCE, ailmentChance);
            suffixes.Add(Affixes.BURN_CHANCE, ailmentChance);
            suffixes.Add(Affixes.INCREASED_BLEED_CHANCE, increasedAilmentChance);
            suffixes.Add(Affixes.INCREASED_BURN_CHANCE, increasedAilmentChance);
        }

        public void InitializeAffixGroup(EquipmentType equipmentType, Affixes[] affixGroup)
        {
            AffixGroup newAffixGroup = new();

            foreach (var affix in affixGroup)
            {
                if(prefixes.TryGetValue(affix, out var prefix)) newAffixGroup.Prefixes.Add(affix, prefix);
                else if(suffixes.TryGetValue(affix, out var suffix)) newAffixGroup.Suffixes.Add(affix, suffix);
                else Debug.LogWarning("Unknown affix group: " + affix);
            }
            
            affixGroups.Add(equipmentType, newAffixGroup);
        }
        

        public static Affix GenerateAffix(AffixType affixType, EquipmentType equipmentType, Affix affix = null)
        {

            Dictionary<Affixes, Vector2Int[]> affixesCopy;
            affixesCopy = affixType == AffixType.PREFIX ? new Dictionary<Affixes, Vector2Int[]>(affixGroups[equipmentType].Prefixes) 
                : new Dictionary<Affixes, Vector2Int[]>(affixGroups[equipmentType].Suffixes);
            
            if(affix != null) affixesCopy.Remove(affix.Affix1);
            int affixIndex = Random.Range(0, affixesCopy.Count);
            Affixes aff = affixesCopy.Keys.ToList()[affixIndex];
            int tier = Random.Range(0, 3);
            int value = affixType == AffixType.PREFIX ? Utils.GetRandomValueFromBase(prefixes[aff][tier]) 
                : Utils.GetRandomValueFromBase(suffixes[aff][tier]);
            
            return new Affix(affixType, aff, value, (byte)tier);
        }
        
    }
}