using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    [CreateAssetMenu(fileName = "ArmorBase", menuName = "EquipmentBases/ArmorBase", order = 0)]
    public class SArmourBase : SEquipmentBase
    {
        [SerializeField] private Vector2Int armour; //min, max
        [SerializeField] private Vector2Int magicResistance; //min max
        [SerializeField] private Vector2Int dodge; //min max

        [SerializeField] public Vector2Int Armour => armour;
        [SerializeField] public Vector2Int MagicResistance => magicResistance;
        [SerializeField] public Vector2Int Dodge => dodge;
    }
}