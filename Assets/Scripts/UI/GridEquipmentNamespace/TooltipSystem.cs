using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.GridEquipmentNamespace
{
    public class TooltipSystem : MonoBehaviour
    {
        public static TooltipSystem instance;
        public GameObject tooltip;

        private TMP_Text textComponent;
        
        public RectTransform tooltipRect;
        public Canvas tooltipCanvas;   
        public Camera uiCamera;
        public ToolTipTrigger currentTrigger;

        private void Awake()
        {
            textComponent = tooltip.transform.GetChild(0).GetComponent<TMP_Text>();
            instance = this;
        }

        public void ShowTooltip(string contentText, RectTransform target, ToolTipTrigger currentTrigger)
        {
            tooltip.SetActive(true);
            textComponent.text = contentText;
            /*textComponent.ForceMeshUpdate();
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.transform as RectTransform);*/
            
            this.currentTrigger = currentTrigger;
            
            StartCoroutine(DelayedPositioning(target));
            
            //ShowTooltipAt(target);
        }

        public void HideTooltip()
        {
            textComponent.text = "";
            currentTrigger = null;
            tooltip.SetActive(false);
        }

        public void ShowTooltipAt(RectTransform target)
        {
            Vector2 offset = new Vector2(
                tooltipRect.sizeDelta.x / 2f,
                tooltipRect.sizeDelta.y / -2f
                );
            
            Vector3 worldPos = target.TransformPoint(target.rect.max);
            Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, worldPos);
            
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                (RectTransform)tooltipCanvas.transform,
                screenPos,
                uiCamera,
                out Vector2 localPoint
                );
            
            tooltipRect.anchoredPosition = localPoint + offset;
        }
        
        private IEnumerator DelayedPositioning(RectTransform target)
        {
            yield return new WaitForEndOfFrame();
            
            LayoutRebuilder.ForceRebuildLayoutImmediate(tooltip.transform as RectTransform);

            ShowTooltipAt(target);
        }

        public bool IsTooltipActive()
        {
            return tooltipRect.gameObject.activeSelf;
        }
        
        
    }
}