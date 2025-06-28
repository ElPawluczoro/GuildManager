using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Characters.Player.CharacterItem;
using UnityEngine;

namespace Characters.Player
{
    public class CharacterEquipment : MonoBehaviour
    {
        private EquipableItem helmet;
        private EquipableItem bodyArmour;
        private EquipableItem boots;
        private EquipableItem gloves;
        private EquipableItem ring1;
        private EquipableItem ring2;
        private EquipableItem amulet;
        private EquipableItem mainHand;
        private EquipableItem offHand;

        public EquipableItem Helmet => helmet;

        public EquipableItem BodyArmour => bodyArmour;

        public EquipableItem Boots => boots;

        public EquipableItem Gloves => gloves;

        public EquipableItem Ring1 => ring1;

        public EquipableItem Ring2 => ring2;

        public EquipableItem Amulet => amulet;

        public EquipableItem MainHand => mainHand;

        public EquipableItem OffHand => offHand;

        public void foo(EquipableItem equipable)
        {
            switch (equipable.EquipmentType) //TODO adjust unequip
            {
                case EquipmentType.HELMET:
                {
                    Equip(ref helmet, equipable);
                    break;
                }
                case EquipmentType.BODY_ARMOUR:
                    Equip(ref bodyArmour, equipable);
                    break;
                case EquipmentType.BOOTS:
                    Equip(ref boots, equipable);
                    break;
                case EquipmentType.GLOVES:
                    Equip(ref gloves, equipable);
                    break;
                case EquipmentType.RING:
                    if(ring1 && !ring2) Equip(ref ring2, equipable);
                    else if(!ring1 && ring2) Equip(ref ring1, equipable);
                    else Equip(ref ring1, equipable);
                    break;
                case EquipmentType.AMULET:
                    Equip(ref amulet, equipable);
                    break;
                case EquipmentType.TWO_HANDED:
                case EquipmentType.BOW:
                    if(offHand) Destroy(offHand.gameObject);
                    Equip(ref mainHand, equipable);
                    break;
                case EquipmentType.MAIN_HAND_WEAPON:
                case EquipmentType.MAIN_HAND_MAGIC_WEAPON:
                    Equip(ref mainHand, equipable);
                    break;
                case EquipmentType.OFF_HAND_WEAPON:
                case EquipmentType.OFF_HAND_SHIELD:
                //case EquipmentType.OFF_HAND_MAGIC_ITEM:
                    Equip(ref offHand, equipable);
                    break;
                
            }
        }

        public void Equip(ref EquipableItem slot, EquipableItem equipable)
        {
            //TODO check for stats requirements
            if (slot) Destroy(slot.gameObject); //TODO create unequip mechanic
            slot = equipable;
            
            
            
        }
    }
}
