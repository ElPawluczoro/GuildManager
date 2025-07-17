using System;
using System.Collections.Generic;
using Characters.Player.CharacterItem;
using UI.CharacterPanel;
using UnityEngine;
using UnityEngine.EventSystems;
using Utilities;

namespace UI.GridEquipment
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
                    Grid grid = gridSlot.transform.parent.GetComponent<Grid>();
                    if (grid.GridBackend.TryPlaceItem(Utils.TranslateIndexToPosition(gridSlot.index, grid.SizeX),
                            itemInUI))
                    {
                        if (itemInUI.currentSlot)
                        {
                            itemInUI.currentSlot.UnequipItem(GetComponent<EquipableItem>());
                            itemInUI.currentSlot = null;
                        }
                    }
                    else
                    {
                        grid.GridBackend.TryPlaceItem(
                            Utils.TranslateIndexToPosition(itemInUI.currentPlacementIndex, grid.SizeX),
                            itemInUI);
                    }
                    return;
                }
                
                var equipmentSlot = result.gameObject.GetComponent<EquipmentSlot>();
                if (equipmentSlot && GetComponent<EquipableItem>() is EquipableItem equipable)
                {
                    if (!equipmentSlot.IsAvailable(equipable))
                    {
                        ReturnToLastGrid(itemInUI);
                        return;
                    }

                    if (equipable.EquipmentType is EquipmentType.TWO_HANDED or EquipmentType.BOW 
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EquipmentSlotType.OFF_HAND].transform.childCount != 0)
                    {
                        if (!itemInUI.currentGrid.PlaceItem
                            (equipmentSlot.transform.parent
                                .GetComponent<CharacterEquipmentUI>()
                                .equipmentSlots[EquipmentSlotType.OFF_HAND]
                                .transform.GetChild(0).GetComponent<ItemInUI>()))
                        {
                            ReturnToLastGrid(itemInUI);
                            return;
                        }
                    }
                    
                    if (equipable.EquipmentType is EquipmentType.OFF_HAND_SHIELD or EquipmentType.OFF_HAND_WEAPON 
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EquipmentSlotType.MAIN_HAND].transform.childCount != 0
                        && equipmentSlot.transform.parent.GetComponent<CharacterEquipmentUI>().equipmentSlots[EquipmentSlotType.MAIN_HAND].transform.GetChild(0)
                            .GetComponent<EquipableItem>().EquipmentType is EquipmentType.TWO_HANDED or EquipmentType.BOW)
                    {
                        if (!itemInUI.currentGrid.PlaceItem
                            (equipmentSlot.transform.parent
                                .GetComponent<CharacterEquipmentUI>()
                                .equipmentSlots[EquipmentSlotType.MAIN_HAND]
                                .transform.GetChild(0).GetComponent<ItemInUI>()))
                        {
                            ReturnToLastGrid(itemInUI);
                            return;
                        }
                    }
                    
                    if (equipmentSlot.transform.childCount == 0)
                    {
                        equipmentSlot.TryPlaceItem(equipable);
                        return;
                    }
                    if (itemInUI.currentGrid.PlaceItem(equipmentSlot.transform.GetChild(0).GetComponent<ItemInUI>()))
                    {
                        equipmentSlot.PlaceItem(equipable);
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


















