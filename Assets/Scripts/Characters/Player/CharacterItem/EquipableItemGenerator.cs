using System;
using System.Linq;
using Characters.Player.CharacterItem.SEquipmentBases;
using ProjectEnums;
using UI.GridEquipmentNamespace;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace Characters.Player.CharacterItem
{
    public class EquipableItemGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;

        [SerializeField] private Transform itemParent;
        
        private Affixes[] defensivesLocal = new []
        {
            Affixes.INCREASED_ARMOUR_LOCAL, Affixes.INCREASED_DODGE_LOCAL, Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL
        };
        
        public static Rarity GetRandomRarity()
        {
            float roll = Random.Range(0f, 1f) * 100f;
            
            if (roll < 1f) return Rarity.COMMON; //TODO change chances after tests
            else if (roll < 50f) return Rarity.MAGIC;
            else return Rarity.RARE;
        }

        public static byte GetRandomNumberOfAffixes(Rarity rarity)
        {
            if(rarity == Rarity.COMMON) return 0;
            if (rarity == Rarity.MAGIC) return (byte)Random.Range(1, 3);
            return (byte)Random.Range(3, 5);
        }
        
        public void GenerateItem(int tier) //Should return gameobject?
        {
            SEquipmentBase equipmentBase = GetBasesTable(tier)[Random.Range(0, GetBasesTable(tier).Length)];
            GameObject newItem = Instantiate(itemPrefab, itemParent);
            EquipableItem equipable = newItem.GetComponent<EquipableItem>();
            Rarity rarity = GetRandomRarity();
            
            newItem.GetComponent<ItemInUI>().SetSize((byte)equipmentBase.Size.x, (byte)equipmentBase.Size.y);
            newItem.GetComponent<ItemInUI>().SetImage(equipmentBase.Icon);
            newItem.GetComponent<EquipableItem>().equipmentBase = equipmentBase;
            
            if (equipmentBase is SArmourBase armourBase)
            {
                int armour = Utils.GetRandomValueFromBase(armourBase.Armour);
                int magicResistance = Utils.GetRandomValueFromBase(armourBase.MagicResistance);
                int dodge = Utils.GetRandomValueFromBase(armourBase.Dodge);
                
                equipable.SetDefensiveProperties(new Vector3Int(armour, magicResistance, dodge));
                if (equipmentBase is SShieldBase shieldBase)
                {
                    byte blockChance = (byte)Utils.GetRandomValueFromBase(shieldBase.BlockChance);
                    equipable.SetShieldProperties(blockChance);
                }
            }
            else if (equipmentBase is SWeaponBase weaponBase)
            {
                int minDamage = Utils.GetRandomValueFromBase(weaponBase.MinDamage);
                int maxDamage = minDamage + weaponBase.MaxDamageAdd;
                float attackSpeed = weaponBase.AttackSpeed;

                equipable.SetOffensiveProperties(new Vector2Int(minDamage, maxDamage), attackSpeed);
            }
            
            equipable.SetProperties
            (
                equipmentBase.BaseName,
                equipmentBase.LevelRequirement,
                equipmentBase.StatRequirements,
                equipmentBase.EquipmentType,
                rarity
            );
            
            int numberOfAffixes = GetRandomNumberOfAffixes(rarity);
            switch (numberOfAffixes)
            {
                case 0:
                    break;
                case 1:
                    if (Random.Range(0, 1) == 1)
                        AddAffix(equipable, AffixType.PREFIX);
                    else AddAffix(equipable, AffixType.SUFFIX);
                    break;
                case 2:
                    AddAffix(equipable, AffixType.PREFIX);
                    AddAffix(equipable, AffixType.SUFFIX);
                    break;
                case 3:
                    AddAffix(equipable, AffixType.PREFIX);
                    AddAffix(equipable, AffixType.SUFFIX);
                    if (Random.Range(0, 1) == 1) AddAffix(equipable, AffixType.PREFIX);
                    else AddAffix(equipable, AffixType.SUFFIX);
                    break;
                case 4:
                    AddAffix(equipable, AffixType.PREFIX);
                    AddAffix(equipable, AffixType.SUFFIX);
                    AddAffix(equipable, AffixType.PREFIX);
                    AddAffix(equipable, AffixType.SUFFIX);
                    break;
                default:
                    Debug.LogError("Somehow there is more than 4 affixes");
                    break;
            }
            
            equipable.GenerateGuid();
            newItem.GetComponent<ItemInUI>().UpdateSize();
            FindAnyObjectByType<GridEquipment>().GridBackend.PlaceItem(newItem.GetComponent<ItemInUI>()); //TODO Consider
            
            
            //return newItem;
        }

        public void AddAffix(EquipableItem equipable, AffixType affixType)
        {
            if (affixType == AffixType.PREFIX)
            {
                Affix pref = equipable.Prefixes.Count > 0 ? equipable.Prefixes[0] : null;
                equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType, pref));
            }
            else
            {
                Affix suff = equipable.Suffixes.Count > 0 ? equipable.Suffixes[0] : null;
                Affix a = AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType, suff);
                equipable.AddSuffix(a);
                if (defensivesLocal.Contains(a.Affix1))
                {
                    HandleLocalDefensives(equipable, a);
                }
            }
        }
        
        
        public void HandleLocalDefensives(EquipableItem equipable, Affix affix)
        {
            int armour = equipable.DefensiveStats.x;
            int magicResistance =equipable.DefensiveStats.y;
            int dodge = equipable.DefensiveStats.z;
            if (affix.Affix1 == Affixes.INCREASED_ARMOUR_LOCAL)
            {
                armour = (int)Math.Floor(equipable.DefensiveStats.x * (1 + affix.Value * 0.01f));
            }
            else if (affix.Affix1 == Affixes.INCREASED_MAGIC_RESISTANCE_LOCAL)
            {
                magicResistance = (int)Math.Floor(equipable.DefensiveStats.y * (1 + affix.Value * 0.01f));
            }
            else if (affix.Affix1 == Affixes.INCREASED_DODGE_LOCAL)
            {
                dodge = (int)Math.Floor(equipable.DefensiveStats.z * (1 + affix.Value * 0.01f));
            }
            equipable.SetTotalDefensive(new Vector3Int(armour, magicResistance, dodge));
        }
        
        public SEquipmentBase[] GetBasesTable(int tier)
        {
            switch (tier)
            {
                case 1:
                    return EquipmentBases.equipmentBasesDatabaseT1;
            } 
            
            Debug.LogWarning($"Something went wrong, equipment bases T1 were returned. Provided {tier} base tier");
            return EquipmentBases.equipmentBasesDatabaseT1;
        }
        
        public EquipableItem GenerateSpecificItem(EquipableItem equipableItem)
        {
            var newItem = Instantiate(itemPrefab, itemParent);
            
            Destroy(newItem.GetComponent<EquipableItem>());
            newItem.AddComponent<EquipableItem>();
            return newItem.GetComponent<EquipableItem>();
        }
    }
}




















