using System;
using System.Collections.Generic;
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
    }
    
    
}