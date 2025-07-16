using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    [CreateAssetMenu(fileName = "WeaponBase", menuName = "EquipmentBases/WeaponBase", order = 0)]
    public class SWeaponBase : SEquipmentBase
    {
        [SerializeField] private Vector2Int minDamage;
        [SerializeField] private int maxDamageAdd;
        [SerializeField] private float attackSpeed;

        public Vector2Int MinDamage => minDamage;

        public int MaxDamageAdd => maxDamageAdd;

        public float AttackSpeed => attackSpeed;
    }
}