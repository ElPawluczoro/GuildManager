using System;
using System.Collections.Generic;
using Characters.Player.CharacterItem;
using Characters.Player.CharacterItem.SEquipmentBases;
using ProjectEnums;
using UI.CharacterPanel;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace UI.GridEquipmentNamespace
{
    public class UiDragItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Vector2 originalPosition;

        [SerializeField] private Canvas canvas;
        private Vector2 pointerOfSet;

        private Vector2 size;
        private Vector2 topLeftLocal;
        
        private void SetUpDragItem()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            originalPosition = rectTransform.anchoredPosition;
            canvas = FindAnyObjectByType<Canvas>();
        }

        public void SetTopLeftPoint(Vector2 _size)
        {
            SetUpDragItem();
            size = _size;

            Vector2 offset = new Vector2(25f / 2f, -25f / 2f);
            topLeftLocal = rectTransform.anchoredPosition + new Vector2(-size.x / 2f, size.y / 2f) + offset;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(canvas.transform);
            transform.SetAsLastSibling();
            canvasGroup.alpha = 0.6f;
            canvasGroup.blocksRaycasts = false;
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, 
                eventData.position,
                canvas.worldCamera,
                out pointerOfSet
            );
            
            pointerOfSet = rectTransform.anchoredPosition - pointerOfSet;

            var curGrid = GetComponent<ItemInUI>().currentGrid;
            GetComponent<ItemInUI>().currentGrid.RemoveItem(GetComponent<ItemInUI>());
        }

        public void OnDrag(PointerEventData eventData)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform, 
                eventData.position,
                canvas.worldCamera,
                out Vector2 localPoint
            );
            rectTransform.anchoredPosition = localPoint + pointerOfSet;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = true;
            
            Vector3 worldTopLeft = rectTransform.TransformPoint(topLeftLocal);
            Vector2 screenTopLet = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, worldTopLeft); 
            
            PointerEventData pointer = new PointerEventData(EventSystem.current) {position = screenTopLet};
            
            List<RaycastResult> results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointer, results);
            var itemInUI = GetComponent<ItemInUI>();
            
            foreach (RaycastResult result in results)
            {
                var gridSlot = result.gameObject.GetComponent<GridSlot>();
                if (gridSlot)
                {
                    GridEquipment gridEquipment = gridSlot.transform.parent.GetComponent<GridEquipment>();
                    if (gridEquipment.GridBackend.TryPlaceItem(Utils.TranslateIndexToPosition(gridSlot.index, gridEquipment.SizeX),
                            itemInUI))
                    {
                        if (itemInUI.currentSlot)
                        {
                            itemInUI.currentSlot.UnequipItem(GetComponent<EquipableHolder>().EquipableItem);
                            itemInUI.currentSlot = null;
                        }
                    }
                    else
                    {
                        gridEquipment.GridBackend.TryPlaceItem(
                            Utils.TranslateIndexToPosition(itemInUI.currentPlacementIndex, gridEquipment.SizeX),
                            itemInUI);
                    }
                    return;
                }
                
                var equipmentSlot = result.gameObject.GetComponent<EquipmentSlot>();
                if (equipmentSlot && GetComponent<EquipableHolder>() is EquipableHolder equipableHolder)
                {
                    if (!equipmentSlot.IsAvailable(equipableHolder.EquipableItem))
                    {
                        ReturnToLastGrid(itemInUI);
                        return;
                    }

                    if (equipableHolder.EquipableItem.EquipmentType is EquipmentType.TWO_HANDED or EquipmentType.BOW 
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EEquipmentSlotType.OFF_HAND].transform.childCount != 0)
                    {
                        if (!itemInUI.currentGrid.PlaceItem
                            (equipmentSlot.transform.parent
                                .GetComponent<CharacterEquipmentUI>()
                                .equipmentSlots[EEquipmentSlotType.OFF_HAND]
                                .transform.GetChild(0).GetComponent<ItemInUI>()))
                        {
                            ReturnToLastGrid(itemInUI);
                            return;
                        }
                    }
                    
                    if (equipableHolder.EquipableItem.EquipmentType is EquipmentType.OFF_HAND_SHIELD or EquipmentType.OFF_HAND_WEAPON 
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EEquipmentSlotType.MAIN_HAND].transform.childCount != 0
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EEquipmentSlotType.MAIN_HAND].transform.GetChild(0)
                            .GetComponent<EquipableHolder>().EquipableItem.EquipmentType is EquipmentType.TWO_HANDED or EquipmentType.BOW)
                    {
                        if (!itemInUI.currentGrid.PlaceItem
                            (equipmentSlot.transform.parent
                                .GetComponent<CharacterEquipmentUI>()
                                .equipmentSlots[EEquipmentSlotType.MAIN_HAND]
                                .transform.GetChild(0).GetComponent<ItemInUI>()))
                        {
                            ReturnToLastGrid(itemInUI);
                            return;
                        }
                    }
                    
                    if (equipmentSlot.transform.childCount == 0)
                    {
                        equipmentSlot.TryPlaceItem(equipableHolder);
                        return;
                    }
                    if (itemInUI.currentGrid.PlaceItem(equipmentSlot.transform.GetChild(0).GetComponent<ItemInUI>()))
                    {
                        equipmentSlot.PlaceItem(equipableHolder);
                        return;   
                    }
                    ReturnToLastGrid(itemInUI);
                    return;
                }
            }

            var currentSlot = GetComponent<ItemInUI>().currentSlot;
            if (currentSlot)
            {
                currentSlot.FitItemInSlot(rectTransform);
                return;
            }
            
            ReturnToLastGrid(itemInUI);
            
        }

        private static bool ReturnToLastGrid(ItemInUI itemInUI)
        {
            return itemInUI.currentGrid.TryPlaceItem(
                Utils.TranslateIndexToPosition(itemInUI.currentPlacementIndex, itemInUI.currentGrid.Grid.GetLength(0)),
                itemInUI);
        }
    }
}


















