using System;
using System.Collections.Generic;
using System.Reflection;
using Characters.Player.CharacterItem;
using ProjectEnums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Utilities
{
    public class Utils
    {
        public static Dictionary<Affixes, (bool isBasic, Enum stat)> AffixToStatMap = new()
        {
            // Basic
            { Affixes.FLAT_STRENGTH, (true, ECharacterBasicStat.STRENGTH) },
            { Affixes.FLAT_DEXTERITY, (true, ECharacterBasicStat.DEXTERITY) },
            { Affixes.FLAT_INTELLIGENCE, (true, ECharacterBasicStat.INTELLIGENCE) },
            { Affixes.ARMOUR, (true, ECharacterBasicStat.ARMOUR) },
            { Affixes.MAGIC_RESISTANCE, (true, ECharacterBasicStat.MAGIC_RESISTANCE) },
            { Affixes.DODGE, (true, ECharacterBasicStat.DODGE) },
            { Affixes.FLAT_VITALITY, (true, ECharacterBasicStat.VITALITY) },

            // Advanced
            { Affixes.INCREASED_STRENGTH, (false, EAdvancedStat.INCREASED_STRENGTH) },
            { Affixes.INCREASED_DEXTERITY, (false, EAdvancedStat.INCREASED_DEXTERITY) },
            { Affixes.INCREASED_INTELLIGENCE, (false, EAdvancedStat.INCREASED_INTELLIGENCE) },
            { Affixes.INCREASED_HEALTH, (false, EAdvancedStat.INCREASED_HEALTH) },
            { Affixes.ADDITIONAL_PROJECTILES, (false, EAdvancedStat.ADDITIONAL_PROJECTILES) },
            { Affixes.INCREASED_DAMAGE, (false, EAdvancedStat.INCREASED_DAMAGE) },
            { Affixes.INCREASED_ARMOUR_GLOBAL, (false, EAdvancedStat.INCREASED_ARMOUR) },
            { Affixes.INCREASED_MAGIC_RESISTANCE_GLOBAL, (false, EAdvancedStat.INCREASED_MAGIC_RESISTANCE) },
            { Affixes.INCREASED_DODGE_GLOBAL, (false, EAdvancedStat.INCREASED_DODGE) },
            { Affixes.DAMAGE_REDUCTION, (false, EAdvancedStat.DAMAGE_REDUCTION) },
            { Affixes.BLOCK_CHANCE, (false, EAdvancedStat.BLOCK_CHANCE) },
            { Affixes.BLOCK_DAMAGE_REDUCTION, (false, EAdvancedStat.BLOCK_DAMAGE_REDUCTION) },
            { Affixes.HEALTH_REGEN, (false, EAdvancedStat.HEALTH_REGEN) },
            { Affixes.MANA_REGEN, (false, EAdvancedStat.MANA_REGEN) },
            { Affixes.INCREASED_PHYSICAL_DAMAGE, (false, EAdvancedStat.INCREASED_PHYSICAL_DAMAGE) },
            { Affixes.INCREASED_MAGIC_DAMAGE, (false, EAdvancedStat.INCREASED_MAGIC_DAMAGE) },
            { Affixes.INCREASED_SPELL_SKILL_DAMAGE, (false, EAdvancedStat.INCREASED_SPELL_SKILL_DAMAGE) },
            { Affixes.INCREASED_ATTACK_SKILL_DAMAGE, (false, EAdvancedStat.INCREASED_ATTACK_SKILL_DAMAGE) },
            { Affixes.INCREASED_ATTACK_RANGE, (false, EAdvancedStat.INCREASED_ATTACK_RANGE) },
            { Affixes.INCREASED_ATTACK_SPEED, (false, EAdvancedStat.INCREASED_ATTACK_SPEED) },
            { Affixes.INCREASED_CAST_SPEED, (false, EAdvancedStat.INCREASED_CAST_SPEED) },
            { Affixes.ARMOUR_PENETRATION, (false, EAdvancedStat.ARMOUR_PENETRATION) },
            { Affixes.MAGIC_DAMAGE_PENETRATION, (false, EAdvancedStat.MAGIC_DAMAGE_PENETRATION) },
            { Affixes.LIFE_STEAL, (false, EAdvancedStat.LIFE_STEAL) },
            { Affixes.BLEED_CHANCE, (false, EAdvancedStat.BLEED_CHANCE) },
            { Affixes.BURN_CHANCE, (false, EAdvancedStat.BURN_CHANCE) },
            { Affixes.INCREASED_BLEED_CHANCE, (false, EAdvancedStat.INCREASED_BLEED_CHANCE) },
            { Affixes.INCREASED_BURN_CHANCE, (false, EAdvancedStat.INCREASED_BURN_CHANCE) },
            { Affixes.INCREASED_SKILL_DURATION, (false, EAdvancedStat.INCREASED_SKILL_DURATION) },
        };

        public static int GetRandomValueFromBase(Vector2Int baseStats)
        {
            return Random.Range(baseStats.x, baseStats.y);
        }

        public static int TranslatePositionToIndex(Vector2Int position, int columnSize)
        {
            return columnSize * position.y + position.x;
        }

        public static Vector2Int TranslateIndexToPosition(int index, int columnSize)
        {
            return new Vector2Int(index % columnSize, index / columnSize);
        }

        public static string[] AffixToString(Affixes affix)
        {
            switch (affix)
            {
                case Affixes.FLAT_STRENGTH: return new string[] { "strength", "" };
                case Affixes.FLAT_DEXTERITY: return new string[] { "dexterity", "" };
                case Affixes.FLAT_INTELLIGENCE: return new string[] { "intelligence", "" };
                case Affixes.FLAT_VITALITY: return new string[] { "vitality", "" };
                case Affixes.HEALTH_REGEN: return new string[] { "health regeneration", "" };
                case Affixes.MANA_REGEN: return new string[] { "mana regeneration", "" };
                case Affixes.INCREASED_PHYSICAL_DAMAGE: return new string[] { "increased physical damage", "%" };
                case Affixes.INCREASED_MAGIC_DAMAGE: return new string[] { "increased magic damage", "%" };
                case Affixes.INCREASED_SPELL_SKILL_DAMAGE: return new string[] { "increased spell damage", "%" };
                case Affixes.INCREASED_ATTACK_SKILL_DAMAGE: return new string[] { "increased attack skill damage", "%" };
                case Affixes.INCREASED_ATTACK_RANGE: return new string[] { "increased attack range", "%" };
                case Affixes.INCREASED_ATTACK_SPEED: return new string[] { "increased attack speed", "%" };
                case Affixes.INCREASED_CAST_SPEED: return new string[] { "increased cast speed", "%" };
                case Affixes.LIFE_STEAL: return new string[] { "life steal", "%" };
                case Affixes.ARMOUR_PENETRATION: return new string[] { "armor penetration", "%" };
                case Affixes.MAGIC_DAMAGE_PENETRATION: return new string[] { "magic penetration", "%" };
                case Affixes.INCREASED_SKILL_DURATION: return new string[] { "increased skill duration", "%" };
                case Affixes.INCREASED_DAMAGE: return new string[] { "increased damage", "%" };
                case Affixes.INCREASED_ARMOUR_LOCAL: return new string[] { "increased armor", "%" };
                case Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL: return new string[] { "increased magic resistance", "%" };
                case Affixes.INCREASED_DODGE_LOCAL: return new string[] { "increased doge", "%" };
                case Affixes.ARMOUR: return new string[] { "armor", "" };
                case Affixes.MAGIC_RESISTANCE: return new string[] { "magic resistance", "" };
                case Affixes.DODGE: return new string[] { "doge", "" };
                case Affixes.DAMAGE_REDUCTION: return new string[] { "damage reduction", "%" };
                case Affixes.BLEED_CHANCE: return new string[] { "bleed chance", "%" };
                case Affixes.BURN_CHANCE: return new string[] { "burn chance", "%" };
                case Affixes.INCREASED_BLEED_CHANCE: return new string[] { "increased bleed chance", "%" };
                case Affixes.INCREASED_BURN_CHANCE: return new string[] { "increased burn chance", "%" };

                default: return new string[] { affix.ToString(), "" };

            }
        }
        
        public static void CopyValues<T>(T source, T destination)
        {
            if (source == null || destination == null)
                return;

            BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var type = typeof(T);

            while (type != null && type != typeof(MonoBehaviour))
            {
                foreach (var field in type.GetFields(flags))
                {
                    // Pomijaj pola Unity'owe (np. gameObject, transform, tag itp.)
                    if (field.IsDefined(typeof(SerializeField), false) || field.IsPublic)
                    {
                        field.SetValue(destination, field.GetValue(source));
                    }
                }

                type = type.BaseType;
            }
        }
    }
}