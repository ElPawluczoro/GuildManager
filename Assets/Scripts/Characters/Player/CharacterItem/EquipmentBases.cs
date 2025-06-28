using System;
using System.Collections.Generic;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Characters.Player.CharacterItem
{
    public class EquipmentBases : MonoBehaviour
    {
        private List<EquipmentBase> equipmentBasesT1 = new List<EquipmentBase>();

        public static EquipmentBase[] equipmentBasesDatabaseT1;

        private void Awake()
        {
            GenerateBases();
        }

        public void GenerateBases()
        {
            // T1
            // HELMETS
            equipmentBasesT1.Add(new ArmourBase("Light helmet", new Vector3Int(3, 0, 0), 1, 
                EquipmentType.HELMET,
                new Vector2Int(2, 5), Vector2Int.zero, Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("Light hat", new Vector3Int(0, 0, 3), 1, 
                EquipmentType.HELMET,
                Vector2Int.zero, new Vector2Int(2, 5), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("dodge hat", new Vector3Int(0, 3, 0), 1, 
                EquipmentType.HELMET,
                Vector2Int.zero, Vector2Int.zero, new Vector2Int(2, 5)));
            equipmentBasesT1.Add(new ArmourBase("mr a hat", new Vector3Int(2, 0, 2), 1, 
                EquipmentType.HELMET,
                new Vector2Int(1, 3), new Vector2Int(1, 3), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("a dodge hat", new Vector3Int(3, 3, 0), 1,
                EquipmentType.HELMET,
                new Vector2Int(1, 3), Vector2Int.zero, new Vector2Int(1, 3)));
            equipmentBasesT1.Add(new ArmourBase("enchanted leather hat", new Vector3Int(2, 0, 2), 1, 
                EquipmentType.HELMET,
                Vector2Int.zero, new Vector2Int(1, 3), new Vector2Int(1, 3)));

            // BODY_ARMOUR
            equipmentBasesT1.Add(new ArmourBase("Light armour", new Vector3Int(5, 0, 0), 1, 
                EquipmentType.BODY_ARMOUR,
                new Vector2Int(3, 7), Vector2Int.zero, Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("Light cloak", new Vector3Int(0, 0, 5), 1,
                EquipmentType.BODY_ARMOUR,
                Vector2Int.zero, new Vector2Int(3, 7), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("Light leather armour", new Vector3Int(0, 5, 0), 1,
                EquipmentType.BODY_ARMOUR,
                Vector2Int.zero, Vector2Int.zero, new Vector2Int(3, 7)));
            equipmentBasesT1.Add(new ArmourBase("light chainmail", new Vector3Int(3, 0, 3), 1, 
                EquipmentType.BODY_ARMOUR,
                new Vector2Int(2, 4), new Vector2Int(2, 4), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("reinforced leather armour", new Vector3Int(3, 3, 0), 1,
                EquipmentType.BODY_ARMOUR,
                new Vector2Int(2, 4), Vector2Int.zero, new Vector2Int(2, 4)));
            equipmentBasesT1.Add(new ArmourBase("enchanted leather armour", new Vector3Int(3, 0, 3), 1,
                EquipmentType.BODY_ARMOUR,
                Vector2Int.zero, new Vector2Int(2, 4), new Vector2Int(2, 4)));

            // BOOTS
            equipmentBasesT1.Add(new ArmourBase("Light boots", new Vector3Int(3, 0, 0), 1, 
                EquipmentType.BOOTS,
                new Vector2Int(2, 5), Vector2Int.zero, Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("Light boots", new Vector3Int(0, 0, 3), 1, 
                EquipmentType.BOOTS,
                Vector2Int.zero, new Vector2Int(2, 5), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("dodge boots", new Vector3Int(0, 3, 0), 1, 
                EquipmentType.BOOTS,
                Vector2Int.zero, Vector2Int.zero, new Vector2Int(2, 5)));
            equipmentBasesT1.Add(new ArmourBase("mr a boots", new Vector3Int(2, 0, 2), 1, 
                EquipmentType.BOOTS,
                new Vector2Int(1, 3), new Vector2Int(1, 3), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("a dodge boots", new Vector3Int(3, 3, 0), 1, 
                EquipmentType.BOOTS,
                new Vector2Int(1, 3), Vector2Int.zero, new Vector2Int(1, 3)));
            equipmentBasesT1.Add(new ArmourBase("enchanted leather boots", new Vector3Int(2, 0, 2), 1,
                EquipmentType.BOOTS,
                Vector2Int.zero, new Vector2Int(1, 3), new Vector2Int(1, 3)));

            // GLOVES
            equipmentBasesT1.Add(new ArmourBase("Light gloves", new Vector3Int(3, 0, 0), 1,
                EquipmentType.GLOVES,
                new Vector2Int(2, 5), Vector2Int.zero, Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("Light gloves", new Vector3Int(0, 0, 3), 1, 
                EquipmentType.GLOVES,
                Vector2Int.zero, new Vector2Int(2, 5), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("dodge gloves", new Vector3Int(0, 3, 0), 1, 
                EquipmentType.GLOVES,
                Vector2Int.zero, Vector2Int.zero, new Vector2Int(2, 5)));
            equipmentBasesT1.Add(new ArmourBase("mr a gloves", new Vector3Int(2, 0, 2), 1, 
                EquipmentType.GLOVES,
                new Vector2Int(1, 3), new Vector2Int(1, 3), Vector2Int.zero));
            equipmentBasesT1.Add(new ArmourBase("a dodge gloves", new Vector3Int(3, 3, 0), 1, 
                EquipmentType.GLOVES,
                new Vector2Int(1, 3), Vector2Int.zero, new Vector2Int(1, 3)));
            equipmentBasesT1.Add(new ArmourBase("enchanted leather gloves", new Vector3Int(2, 0, 2), 1,
                EquipmentType.GLOVES,
                Vector2Int.zero, new Vector2Int(1, 3), new Vector2Int(1, 3)));

            // RINGS
            equipmentBasesT1.Add(new EquipmentBase("ring", new Vector3Int(0, 0, 0), 1, EquipmentType.RING));
            
            // AMULETS
            equipmentBasesT1.Add(new EquipmentBase("Amulet", new Vector3Int(0, 0, 0), 1, EquipmentType.AMULET));
            
            // TWO_HANDED
            equipmentBasesT1.Add(new WeaponBase("Two handed sword", new Vector3Int(4, 4, 0), 1, EquipmentType.TWO_HANDED,
                new Vector2Int(2, 5), 2, 1.2f));
            equipmentBasesT1.Add(new WeaponBase("Two handed mace", new Vector3Int(7, 2, 0), 1, EquipmentType.TWO_HANDED,
                new Vector2Int(4, 7), 4, 0.8f));
            
            //BOWS
            equipmentBasesT1.Add(new WeaponBase("Short bow", new Vector3Int(0, 7, 0), 1, EquipmentType.BOW,
                new Vector2Int(1, 4), 2, 1.4f));
            equipmentBasesT1.Add(new WeaponBase("Strong bow", new Vector3Int(3, 6, 0), 1, EquipmentType.BOW,
                new Vector2Int(3, 6), 3, 1.0f));
            
            // MAIN_HAND_WEAPONS
            equipmentBasesT1.Add(new WeaponBase("Short sword", new Vector3Int(3, 3, 0), 1, EquipmentType.MAIN_HAND_WEAPON,
                new Vector2Int(2, 3), 2, 1.3f));
            equipmentBasesT1.Add(new WeaponBase("Mace", new Vector3Int(6, 0, 0), 1, EquipmentType.MAIN_HAND_WEAPON,
                new Vector2Int(3, 6), 3, 0.9f));
            
            // MAIN_HAND_MAGIC_WEAPONS
            equipmentBasesT1.Add(new WeaponBase("Sceptre", new Vector3Int(3, 0, 3), 1, EquipmentType.MAIN_HAND_MAGIC_WEAPON,
                new Vector2Int(1, 2), 2, 0.9f));
            equipmentBasesT1.Add(new WeaponBase("Wand", new Vector3Int(0, 0, 6), 1, EquipmentType.MAIN_HAND_MAGIC_WEAPON,
                new Vector2Int(2, 3), 3, 1.0f));
            
            // OFF_HAND_WEAPON
            equipmentBasesT1.Add(new WeaponBase("Dagger", new Vector3Int(0, 4, 0), 1, EquipmentType.OFF_HAND_WEAPON,
                new Vector2Int(1, 2), 1, 1.5f));

            // OFF_HAND_SHIELD
            equipmentBasesT1.Add(new ShieldBase("Round shield", new Vector3Int(6, 0, 0), 1, EquipmentType.OFF_HAND_SHIELD,
                new Vector2Int(2, 5), Vector2Int.zero, Vector2Int.zero, new Vector2Int(10, 15)));
            equipmentBasesT1.Add(new ShieldBase("Enchanted shield", new Vector3Int(0, 0, 6), 1, EquipmentType.OFF_HAND_SHIELD,
                Vector2Int.zero, new Vector2Int(2, 5), Vector2Int.zero, new Vector2Int(10, 15)));
            
            equipmentBasesDatabaseT1 = equipmentBasesT1.ToArray();
        }




    }

    public class EquipmentBase
    {
        private string baseName;
        private Vector3Int statRequirements;
        private byte levelRequirement;
        private EquipmentType equipmentType;

        public string BaseName => baseName;
        public Vector3Int StatRequirements => statRequirements;

        public byte LevelRequirement => levelRequirement;

        public EquipmentType EquipmentType => equipmentType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="baseName"></param>
        /// <param name="statRequirements">str, dex, int</param>
        /// <param name="levelRequirement"></param>
        /// <param name="equipmentType"></param>
        public EquipmentBase(string baseName, Vector3Int statRequirements, byte levelRequirement, EquipmentType equipmentType)
        {
            this.baseName = baseName;
            this.statRequirements = statRequirements;
            this.levelRequirement = levelRequirement;
            this.equipmentType = equipmentType;
        }
    }

    public class ArmourBase : EquipmentBase
    {
        private Vector2Int armour; //min, max
        private Vector2Int magicResistance; //min max
        private Vector2Int dodge; //min max

        public Vector2Int Armour => armour;
        public Vector2Int MagicResistance => magicResistance;
        public Vector2Int Dodge => dodge;

        public ArmourBase(string baseName, Vector3Int statRequirements, byte levelRequirement, EquipmentType equipmentType,
            Vector2Int armour, Vector2Int magicResistance, Vector2Int dodge) 
            : base(baseName, statRequirements, levelRequirement, equipmentType)
        {
            this.armour = armour;
            this.magicResistance = magicResistance;
            this.dodge = dodge;
        }
    }

    public class ShieldBase : ArmourBase
    {
        private Vector2Int blockChance;

        public Vector2Int BlockChance => blockChance;

        public ShieldBase(string baseName, Vector3Int statRequirements, byte levelRequirement, EquipmentType equipmentType,
            Vector2Int armour, Vector2Int magicResistance, Vector2Int dodge, Vector2Int blockChance) 
            : base(baseName, statRequirements, levelRequirement, equipmentType, armour, magicResistance, dodge)
        {
            this.blockChance = blockChance;
        }
    }

    public class WeaponBase : EquipmentBase
    {
        private Vector2Int minDamage;
        private int maxDamageAdd;
        private float attackSpeed;

        public Vector2Int MinDamage => minDamage;

        public int MaxDamageAdd => maxDamageAdd;

        public float AttackSpeed => attackSpeed;

        public WeaponBase(string baseName, Vector3Int statRequirements, byte levelRequirement, EquipmentType equipmentType,
            Vector2Int minDamage, int maxDamageAdd, float attackSpeed) : base(baseName, statRequirements, levelRequirement,
            equipmentType)
        {
            this.minDamage = minDamage;
            this.maxDamageAdd = maxDamageAdd;
            this.attackSpeed = attackSpeed;
        }
    }
        
}