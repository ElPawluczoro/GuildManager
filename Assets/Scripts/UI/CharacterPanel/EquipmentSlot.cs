using System;
using System.Collections.Generic;
using Characters.Player;
using Characters.Player.CharacterItem;
using UI.GridEquipmentNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace UI.CharacterPanel
{
    public class EquipmentSlot : MonoBehaviour
    {
        [SerializeField] private byte cellSize = 30;
        [SerializeField] private byte sizeX;
        [SerializeField] private byte sizeY;
        [SerializeField] private Sprite icon;
        [SerializeField] List<EquipmentType> availableItemTypes = new List<EquipmentType>();
        [SerializeField] private CharacterEquipment characterEquipment;

        public CharacterEquipment CharacterEquipment => characterEquipment;

        private EquipableItem lastChild;
        
        private void OnEnable()
        {
            SetUpSlot();
        }

        private void SetUpSlot()
        {
            GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * cellSize, sizeY* cellSize);
            GetComponent<Image>().sprite = icon;
        }

        public void FitItemInSlot(RectTransform itemRect)
        {
            itemRect.SetParent(transform);
            itemRect.anchoredPosition = new Vector2(0, 0);
            itemRect.anchoredPosition += new Vector2(cellSize * sizeX / 2f, -cellSize * sizeY / 2f);
        }

        public bool TryPlaceItem(EquipableItem equipableItem)
        {
            if (!IsAvailable(equipableItem)) return false;

            return PlaceItem(equipableItem);
        }

        public bool PlaceItem(EquipableItem equipableItem)
        {
            if (equipableItem != lastChild) characterEquipment.foo(equipableItem);
            equipableItem.GetComponent<ItemInUI>().currentSlot = this;
            FitItemInSlot(equipableItem.GetComponent<RectTransform>());
            lastChild = equipableItem;
            return true;
        }

        public bool IsAvailable(EquipableItem equipableItem)
        {
            if (!availableItemTypes.Contains(equipableItem.EquipmentType)) return false;
            if (!characterEquipment.CheckStatRequirements(equipableItem)) return false;
            return true;
        }

        public void UnequipItem(EquipableItem equipableItem)
        {
            characterEquipment.foo(equipableItem);
            lastChild = null;
        }
        
    }
}











