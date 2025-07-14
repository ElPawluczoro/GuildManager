using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Characters.Player.CharacterItem;
using ProjectEnums;
using UnityEngine;
using Utilities;

namespace Characters.Player
{
    public class CharacterEquipment : MonoBehaviour
    {
        [SerializeField] private EquipableItem helmet;
        [SerializeField] private EquipableItem bodyArmour;
        [SerializeField] private EquipableItem boots;
        [SerializeField] private EquipableItem gloves;
        [SerializeField] private EquipableItem ring1;
        [SerializeField] private EquipableItem ring2;
        [SerializeField] private EquipableItem amulet;
        [SerializeField] private EquipableItem mainHand;
        [SerializeField] private EquipableItem offHand;
        
        private CharacterBasicStats basicStats;
        private CharacterAdvancedStats advancedStats;
        private CharacterHealth characterHealth;

        public EquipableItem Helmet => helmet;

        public EquipableItem BodyArmour => bodyArmour;

        public EquipableItem Boots => boots;

        public EquipableItem Gloves => gloves;

        public EquipableItem Ring1 => ring1;

        public EquipableItem Ring2 => ring2;

        public EquipableItem Amulet => amulet;

        public EquipableItem MainHand => mainHand;

        public EquipableItem OffHand => offHand;

        private void Start()
        {
            basicStats = GetComponent<CharacterBasicStats>();
            advancedStats = GetComponent<CharacterAdvancedStats>();
            characterHealth = GetComponent<CharacterHealth>();
        }

        public void foo(EquipableItem equipable)
        {
            switch (equipable.EquipmentType) //TODO adjust unequip
            {
                case EquipmentType.HELMET:
                {
                    Equip(ref helmet, equipable);
                    break;
                }
                case EquipmentType.BODY_ARMOUR:
                    Equip(ref bodyArmour, equipable);
                    break;
                case EquipmentType.BOOTS:
                    Equip(ref boots, equipable);
                    break;
                case EquipmentType.GLOVES:
                    Equip(ref gloves, equipable);
                    break;
                case EquipmentType.RING:
                    if(ring1 == equipable) { UnEquip(ref ring1, equipable); break; }
                    else if(ring2 == equipable) { UnEquip(ref ring2, equipable); break; }
                    
                    if(ring1 && !ring2) Equip(ref ring2, equipable);
                    else if(!ring1 && ring2) Equip(ref ring1, equipable);
                    else Equip(ref ring1, equipable);
                    break;
                case EquipmentType.AMULET:
                    Equip(ref amulet, equipable);
                    break;
                case EquipmentType.TWO_HANDED:
                case EquipmentType.BOW:
                    if(offHand) UnEquip(ref offHand,offHand);
                    Equip(ref mainHand, equipable);
                    break;
                case EquipmentType.MAIN_HAND_WEAPON:
                case EquipmentType.MAIN_HAND_MAGIC_WEAPON:
                    Equip(ref mainHand, equipable);
                    break;
                case EquipmentType.OFF_HAND_WEAPON:
                case EquipmentType.OFF_HAND_SHIELD:
                //case EquipmentType.OFF_HAND_MAGIC_ITEM:
                if (mainHand&& mainHand.EquipmentType == EquipmentType.TWO_HANDED ||
                    mainHand.EquipmentType == EquipmentType.BOW)
                {
                    UnEquip(ref mainHand, mainHand);
                }
                    Equip(ref offHand, equipable);
                    break;
                
            }
        }

        public bool CheckStatRequirements(EquipableItem equipable)
        {
            // str, dex, int
            if (equipable.StatRequirements.x > basicStats.BasicStats[ECharacterBasicStat.STRENGTH]) return false;
            if (equipable.StatRequirements.y > basicStats.BasicStats[ECharacterBasicStat.DEXTERITY]) return false;
            if (equipable.StatRequirements.z > basicStats.BasicStats[ECharacterBasicStat.INTELLIGENCE]) return false;
            return true;
        }
        
        public void Equip(ref EquipableItem slot, EquipableItem equipable)
        {
            Affixes[] defensivesLocal =
            {
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_DODGE_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL,
                Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE
            };
            
            if (!CheckStatRequirements(equipable)) return;
            UnEquip(ref slot, slot);
            slot = equipable;

            foreach (Affix prefix in equipable.Prefixes)
            {
                var affixToStat = Utils.AffixToStatMap[prefix.Affix1];
                if (affixToStat.isBasic)
                {
                    basicStats.Modify((ECharacterBasicStat)affixToStat.stat, EOperation.ADD, (short)prefix.Value);
                }
                else
                {
                 advancedStats.Modify((EAdvancedStat)affixToStat.stat, EOperation.ADD, prefix.Value);
                }
            }
            foreach (Affix suffix in equipable.Suffixes)
            {
                if(defensivesLocal.Contains(suffix.Affix1)) continue;
                var affixToStat = Utils.AffixToStatMap[suffix.Affix1];
                if (affixToStat.isBasic)
                {
                    basicStats.Modify((ECharacterBasicStat)affixToStat.stat, EOperation.ADD, (short)suffix.Value);
                }
                else
                {
                    advancedStats.Modify((EAdvancedStat)affixToStat.stat, EOperation.ADD, suffix.Value);
                }
            }

            EquipmentType[] armours =
            {
                EquipmentType.HELMET, EquipmentType.BODY_ARMOUR, EquipmentType.BOOTS, EquipmentType.GLOVES,
                EquipmentType.OFF_HAND_SHIELD
            };
            if (armours.Contains(equipable.EquipmentType))
            {
                HandleDefensives(true, equipable);
            }

            EquipmentType[] weapons =
            {
                EquipmentType.TWO_HANDED, EquipmentType.BOW, EquipmentType.MAIN_HAND_WEAPON,
                EquipmentType.MAIN_HAND_MAGIC_WEAPON, EquipmentType.OFF_HAND_WEAPON
            };
            if (weapons.Contains(equipable.EquipmentType))
            {
                HandleAttackDamage(true, equipable);
            }
        }

        public bool UnEquip(ref EquipableItem slot, EquipableItem equipable)
        {
            Affixes[] defensivesLocal =
            {
                Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_DODGE_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL,
                Affixes.ARMOUR, Affixes.MAGIC_RESISTANCE, Affixes.DODGE
            };
            
            if (!slot) return false; 
            foreach (Affix prefix in equipable.Prefixes)
            {
                var affixToStat = Utils.AffixToStatMap[prefix.Affix1];
                if (affixToStat.isBasic)
                {
                    Debug.Log((short)prefix.Value);
                    basicStats.Modify((ECharacterBasicStat)affixToStat.stat, EOperation.SUBSTRACT, (short)prefix.Value);
                }
                else
                {
                    Debug.Log(prefix.Value);
                    advancedStats.Modify((EAdvancedStat)affixToStat.stat, EOperation.SUBSTRACT, prefix.Value);
                }
            }
            
            foreach (Affix suffix in equipable.Suffixes)
            {
                if(defensivesLocal.Contains(suffix.Affix1)) continue;
                var affixToStat = Utils.AffixToStatMap[suffix.Affix1];
                if (affixToStat.isBasic)
                {
                    Debug.Log((short)suffix.Value);
                    basicStats.Modify((ECharacterBasicStat)affixToStat.stat, EOperation.SUBSTRACT, (short)suffix.Value);
                }
                else
                {
                    Debug.Log(suffix.Value);
                    advancedStats.Modify((EAdvancedStat)affixToStat.stat, EOperation.SUBSTRACT, suffix.Value);
                }
            }
            
            EquipmentType[] armours =
            {
                EquipmentType.HELMET, EquipmentType.BODY_ARMOUR, EquipmentType.BOOTS, EquipmentType.GLOVES,
                EquipmentType.OFF_HAND_SHIELD
            };
            if (armours.Contains(equipable.EquipmentType))
            {
                HandleDefensives(false, equipable);
            }
            
            EquipmentType[] weapons =
            {
                EquipmentType.TWO_HANDED, EquipmentType.BOW, EquipmentType.MAIN_HAND_WEAPON,
                EquipmentType.MAIN_HAND_MAGIC_WEAPON, EquipmentType.OFF_HAND_WEAPON
            };
            if (weapons.Contains(equipable.EquipmentType))
            {
                HandleAttackDamage(false, equipable);
            }
            
            
            
            //Destroy(slot.gameObject); //TODO create unequip mechanic
            slot = null;
            
            return true;
        }

        public void HandleDefensives(bool equip, EquipableItem equipable)
        {
            EOperation operation = equip ? EOperation.ADD : EOperation.SUBSTRACT;
            basicStats.Modify(ECharacterBasicStat.ARMOUR, operation, (short)equipable.DefensiveStats.x);
            basicStats.Modify(ECharacterBasicStat.MAGIC_RESISTANCE, operation, (short)equipable.DefensiveStats.y);
            basicStats.Modify(ECharacterBasicStat.DODGE, operation, (short)equipable.DefensiveStats.z);
        }

        public void HandleAttackDamage(bool equip, EquipableItem equipable)
        {
            EOperation operation = equip ? EOperation.ADD : EOperation.SUBSTRACT;
            basicStats.Modify(ECharacterBasicStat.MIN_ATTACK_DAMAGE, operation, (short)equipable.AttackDamage.x);
            basicStats.Modify(ECharacterBasicStat.MAX_ATTACK_DAMAGE, operation, (short)equipable.AttackDamage.y);
        }

        public void HandleVitality(EquipableItem equipable)
        {
            characterHealth.UpdateMaxHealth();
        }
    }
}










