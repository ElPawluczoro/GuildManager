using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    [CreateAssetMenu(fileName = "ShieldBase", menuName = "EquipmentBases/ShieldBase", order = 0)]
    public class SShieldBase : SEquipmentBase
    {
        [SerializeField] private Vector2Int blockChance;

        public Vector2Int BlockChance => blockChance;
    }
}