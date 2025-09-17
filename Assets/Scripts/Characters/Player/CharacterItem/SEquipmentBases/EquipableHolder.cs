using UnityEngine;

namespace Characters.Player.CharacterItem.SEquipmentBases
{
    public class EquipableHolder : MonoBehaviour
    {
        private EquipableItem equipableItem;
        public EquipableItem EquipableItem => equipableItem;

        public void SetEquipableItem(EquipableItem equipableItem)
        {
            this.equipableItem = equipableItem;
        }
    }
}