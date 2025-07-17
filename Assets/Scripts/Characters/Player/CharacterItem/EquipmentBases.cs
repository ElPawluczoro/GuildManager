using System;
using System.Collections.Generic;
using Characters.Player.CharacterItem.SEquipmentBases;
using UnityEngine;

// ReSharper disable InconsistentNaming

namespace Characters.Player.CharacterItem
{
    public class EquipmentBases : MonoBehaviour
    {
        [SerializeField] private List<SEquipmentBase> equipmentBasesT1 = new List<SEquipmentBase>();

        public static SEquipmentBase[] equipmentBasesDatabaseT1;

        private void Awake()
        {
            equipmentBasesDatabaseT1 = equipmentBasesT1.ToArray();
        }

    }
    
}