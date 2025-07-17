using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI.CharacterPanel
{
    public class CharacterEquipmentUI : MonoBehaviour
    {
        public SerializedDictionary<EquipmentSlotType, EquipmentSlot> equipmentSlots = new()
        {
            { EquipmentSlotType.MAIN_HAND, null },
            { EquipmentSlotType.OFF_HAND, null },
            { EquipmentSlotType.HELMET, null },
            { EquipmentSlotType.BODY_ARMOUR, null },
            { EquipmentSlotType.GLOVES, null },
            { EquipmentSlotType.BOOTS, null },
            { EquipmentSlotType.AMULET, null },
            { EquipmentSlotType.RING1, null },
            { EquipmentSlotType.RING2, null }
        };
    }
        

    
    

    public enum EquipmentSlotType
    {
        MAIN_HAND, OFF_HAND, HELMET, BODY_ARMOUR, GLOVES, BOOTS, AMULET, RING1, RING2
    }
}