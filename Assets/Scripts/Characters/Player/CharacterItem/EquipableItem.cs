// ReSharper disable ParameterHidesMember

using System;
using System.Collections.Generic;
using ProjectEnums;
using UnityEngine;
namespace Characters.Player.CharacterItem
{
    [Serializable]
    public class EquipableItem : Item
    {
        [SerializeField] private EquipmentType equipmentType;
        
        [SerializeField] private List<Affix> prefixes;
        [SerializeField] private List<Affix> suffixes;

        [SerializeField] private Rarity rarity;
        [SerializeField] private byte levelRequirement;
        /// <summary>
        /// str, dex, int
        /// </summary>
        [SerializeField] private Vector3Int statRequirements; //str, dex, int
        [SerializeField] private Vector3Int defensiveStats = Vector3Int.zero; //armour, magic_resistance, dodge
        [SerializeField] private Vector3Int totalDefensiveStats = Vector3Int.zero; //armour, magic_resistance, dodge
        [SerializeField] private Vector2Int attackDamage = Vector2Int.zero; //min, max
        [SerializeField] private float attackSpeed = 0;
        [SerializeField] private byte blockChance = 0;

        [SerializeField] public EquipmentType EquipmentType => equipmentType;

        public List<Affix> Prefixes => prefixes;

        public List<Affix> Suffixes => suffixes;

        public byte LevelRequirement => levelRequirement;

        /// <summary>
        /// str, dex, int
        /// </summary>
        public Vector3Int StatRequirements => statRequirements;

        public Vector3Int DefensiveStats => defensiveStats;
        public Vector3Int TotalDefensiveStats => totalDefensiveStats;

        public Vector2Int AttackDamage => attackDamage;

        public float AttackSpeed => attackSpeed;
        
        public byte BlockChance => blockChance;

        /// <summary>
        /// Jewelery
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="levelRequirement"></param>
        /// <param name="statRequirements">str, dex, int</param>
        /// <param name="equipmentType"></param>
        public void SetProperties(string itemName, byte levelRequirement,
            Vector3Int statRequirements, EquipmentType equipmentType, Rarity rarity)
        {
            this.itemName = itemName;
            this.levelRequirement = levelRequirement;
            this.statRequirements = statRequirements;
            this.equipmentType = equipmentType;
            this.rarity = rarity;
        }

        /// <summary>
        /// Armour
        /// </summary>
        /// <param name="defensiveStats">armour, magic_resistance, dodge</param>
        public void SetDefensiveProperties(Vector3Int defensiveStats)
        {
            this.defensiveStats = defensiveStats;
            this.totalDefensiveStats = defensiveStats;
        }

        public void SetTotalDefensive(Vector3Int defensiveStats)
        {
            this.totalDefensiveStats = defensiveStats;
        }

        /// <summary>
        /// Weapon
        /// </summary>
        /// <param name="attackDamage">min, max</param>
        /// <param name="attackSpeed"></param>
        public void SetOffensiveProperties(Vector2Int attackDamage, float attackSpeed)
        {
            this.attackDamage = attackDamage;
            this.attackSpeed = attackSpeed;
        }

        /// <summary>
        /// Shield
        /// </summary>
        /// <param name="blockChance"></param>
        public void SetShieldProperties(byte blockChance)
        {
            this.blockChance = blockChance;
        }
        
        public void AddPrefix(Affix affix)
        {
            if (prefixes.Count >= 2) //TODO rethink if it is max 3 prefixes. Add somewhere else const maxPrefix/Suffix.
            {
                Debug.LogError("Something is trying to add more than 3 affixes");
                return;
            }
            prefixes.Add(affix);
        }

        public void RemovePrefix(Affix affix)
        {
            if (prefixes.Count <=0 || !prefixes.Contains(affix))
            {
                Debug.LogError("Something is trying to remove prefix that do not exist");
                return;
            }
            prefixes.Remove(affix);
        }
        
        public void AddSuffix(Affix affix)
        {
            if (suffixes.Count >= 2) //TODO rethink if it is max 3 suffixes. Add somewhere else const maxPrefix/Suffix.
            {
                Debug.LogError("Something is trying to add more than 3 suffixes");
                return;
            }
            suffixes.Add(affix);
        }

        public void RemoveSuffix(Affix affix)
        {
            if (suffixes.Count <=0 || !suffixes.Contains(affix))
            {
                Debug.LogError("Something is trying to remove suffix that do not exist");
                return;
            }
            suffixes.Remove(affix);
        }

        public void GenerateGuid()
        {
            guid = Guid.NewGuid();
            Debug.Log(guid);
        }

        public void TestEquip()
        {
            FindAnyObjectByType<CharacterEquipment>().foo(this);
        }

        public static bool operator ==(EquipableItem a, EquipableItem b)
        {
            if(!a || !b) {Debug.Log("false"); return false;}
            Debug.Log((a.Guid == b.Guid));
            
            if(!a || !b) return false;
            return (a.Guid == b.Guid);
        }

        public static bool operator !=(EquipableItem a, EquipableItem b)
        {
            if(!a && b || a && !b) return true;
            return !(a.Guid == b.Guid);
        }
    }
}