using ProjectEnums;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI.CharacterPanel
{
    public class CharacterEquipmentUI : MonoBehaviour
    {
        public SerializedDictionary<EEquipmentSlotType, EquipmentSlot> equipmentSlots = new()
        {
            { EEquipmentSlotType.MAIN_HAND, null },
            { EEquipmentSlotType.OFF_HAND, null },
            { EEquipmentSlotType.HELMET, null },
            { EEquipmentSlotType.BODY_ARMOUR, null },
            { EEquipmentSlotType.GLOVES, null },
            { EEquipmentSlotType.BOOTS, null },
            { EEquipmentSlotType.AMULET, null },
            { EEquipmentSlotType.RING1, null },
            { EEquipmentSlotType.RING2, null }
        };
    }
}