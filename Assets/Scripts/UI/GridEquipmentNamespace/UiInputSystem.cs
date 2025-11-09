using System;
using Characters;
using Gameplay;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GridEquipmentNamespace
{
    public class UiInputSystem : MonoBehaviour
    {
        public static UiInputSystem instance;
        
        [SerializeField] private GameObject inventoryCanvas;
        [SerializeField] private GameObject characterSwitchButton1;
        [SerializeField] private GameObject characterSwitchButton2;
        [SerializeField] private GameObject characterSwitchButton3;
        
        public bool altHeld;

        private void Awake()
        {
            instance = this;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.LeftAlt))
            {
                altHeld = true;
                if(TooltipSystem.instance.IsTooltipActive()) TooltipSystem.instance.currentTrigger.ShowTooltip();
            }

            if (Input.GetKeyUp(KeyCode.LeftAlt))
            {
                altHeld = false;
                if(TooltipSystem.instance.IsTooltipActive()) TooltipSystem.instance.currentTrigger.ShowTooltip();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                inventoryCanvas.SetActive(!inventoryCanvas.activeSelf);
            }
            
        }
    }
}