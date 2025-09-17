using System;
using System.Collections.Generic;
using Characters.Player;
using Characters.Player.CharacterItem;
using Characters.Player.CharacterItem.SEquipmentBases;
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

        public bool TryPlaceItem(EquipableHolder equipableHolder)
        {
            if (!IsAvailable(equipableHolder.EquipableItem)) return false;

            return PlaceItem(equipableHolder);
        }

        public bool PlaceItem(EquipableHolder equipableHolder)
        {
            if (equipableHolder.EquipableItem != lastChild) characterEquipment.foo(equipableHolder.EquipableItem);
            equipableHolder.GetComponent<ItemInUI>().currentSlot = this;
            FitItemInSlot(equipableHolder.GetComponent<RectTransform>());
            lastChild = equipableHolder.EquipableItem;
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











