using ProjectEnums;
using UnityEngine;
using Utilities;
using Vector2 = System.Numerics.Vector2;
using Vector3 = System.Numerics.Vector3;

namespace Characters.Player.CharacterItem
{
    public class EquipableItemGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject itemPrefab;

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
            EquipmentBase equipmentBase = GetBasesTable(tier)[Random.Range(0, GetBasesTable(tier).Length)];
            GameObject newItem = Instantiate(itemPrefab);
            EquipableItem equipable = newItem.GetComponent<EquipableItem>();
            Rarity rarity = GetRandomRarity();
            
            if (equipmentBase is ArmourBase armourBase)
            {
                int armour = Utils.GetRandomValueFromBase(armourBase.Armour);
                int magicResistance = Utils.GetRandomValueFromBase(armourBase.MagicResistance);
                int dodge = Utils.GetRandomValueFromBase(armourBase.Dodge);
                
                equipable.SetDefensiveProperties(new Vector3Int(armour, magicResistance, dodge));
                if (equipmentBase is ShieldBase shieldBase)
                {
                    byte blockChance = (byte)Utils.GetRandomValueFromBase(shieldBase.BlockChance);
                    equipable.SetShieldProperties(blockChance);
                }
            }
            else if (equipmentBase is WeaponBase weaponBase)
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
                    if (Random.Range(0, 1) == 1) equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType));
                    else equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType));
                    break;
                case 2:
                    equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType));
                    equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType));
                    break;
                case 3:
                    equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType));
                    equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType));
                    if (Random.Range(0, 1) == 1) equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType, equipable.Prefixes[0]));
                    else equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType, equipable.Suffixes[0]));
                    break;
                case 4:
                    equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType));
                    equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType));
                    equipable.AddPrefix(AffixGenerator.GenerateAffix(AffixType.PREFIX, equipable.EquipmentType, equipable.Prefixes[0]));
                    equipable.AddSuffix(AffixGenerator.GenerateAffix(AffixType.SUFFIX, equipable.EquipmentType, equipable.Suffixes[0]));
                    break;
                default:
                    Debug.LogError("Shomehow there is more than 4 affixes");
                    break;
            }
            
            
            //return newItem;
        }

        public EquipmentBase[] GetBasesTable(int tier)
        {
            switch (tier)
            {
                case 1:
                    return EquipmentBases.equipmentBasesDatabaseT1;
            } 
            
            Debug.LogWarning($"Something went wrong, equipment bases T1 were returned. Provided {tier} base tier");
            return EquipmentBases.equipmentBasesDatabaseT1;
        }
    }
}




















