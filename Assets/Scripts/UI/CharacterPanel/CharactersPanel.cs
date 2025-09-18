using System;
using System.Collections.Generic;
using Characters.Player;
using Characters.Player.CharacterItem;
using ProjectEnums;
using UnityEngine;

namespace UI.CharacterPanel
{
    public class CharactersPanel : MonoBehaviour
    {
        [SerializeField] private GameObject[] characters = new GameObject[3];

        [SerializeField] private GameObject helmetSlot;
        [SerializeField] private GameObject bodyArmorSlot;
        [SerializeField] private GameObject weaponSlot;
        [SerializeField] private GameObject offHandSlot;
        [SerializeField] private GameObject glovesSlot;
        [SerializeField] private GameObject bootsSlot;

        [SerializeField] private Dictionary<EEquipmentSlotType, GameObject> eqSlots = new();

        [SerializeField] private GameObject[] characterEquipmentSlots = new GameObject[3];
        
        private byte currentCharacter = 0;

        private void Start()
        {
            eqSlots.Add(EEquipmentSlotType.HELMET, helmetSlot);
            eqSlots.Add(EEquipmentSlotType.BODY_ARMOUR, bodyArmorSlot);
            eqSlots.Add(EEquipmentSlotType.MAIN_HAND, weaponSlot);
            eqSlots.Add(EEquipmentSlotType.OFF_HAND, offHandSlot);
            eqSlots.Add(EEquipmentSlotType.GLOVES, glovesSlot);
            eqSlots.Add(EEquipmentSlotType.BOOTS, bootsSlot);
        }

        public void SwitchCharacter()
        {
            ResetEquipmentSlots();
            currentCharacter++;
            if(currentCharacter >= 3) currentCharacter = 0;
            LoadCharacter(characters[currentCharacter]);
        }

        public void SwitchCharacter(int i)
        {
            ResetEquipmentSlots();
            currentCharacter = (byte)i;
            LoadCharacter(characters[currentCharacter]);
        }
        
        public void ResetEquipmentSlots()
        {
            foreach (KeyValuePair<EEquipmentSlotType, GameObject> eqSlot in eqSlots)
            {
                if(eqSlot.Value.transform.childCount == 0) continue;
                Destroy(eqSlot.Value.transform.GetChild(0).gameObject);
            }
        }
        
        public void LoadCharacter(GameObject character)
        {
            EquipableItemGenerator generator = FindAnyObjectByType<EquipableItemGenerator>();
            CharacterEquipment characterEquipment = character.GetComponent<CharacterEquipment>();
            
            foreach (KeyValuePair<EEquipmentSlotType, GameObject> eqSlot in eqSlots)
            {
                eqSlot.Value.GetComponent<EquipmentSlot>().CharacterEquipment = characterEquipment;
                
                Debug.Log(characterEquipment.Get(eqSlot.Key).equipmentBase);
                if(characterEquipment.Get(eqSlot.Key).equipmentBase is null) continue;
                var newItem = generator.InstantiateSpecificEquipable(characterEquipment.Get(eqSlot.Key));
                eqSlot.Value.GetComponent<EquipmentSlot>().FitItemInSlot(newItem.GetComponent<RectTransform>());
            }
        }
    }
}









