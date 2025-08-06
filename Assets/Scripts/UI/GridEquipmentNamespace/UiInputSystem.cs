using System;
using UnityEngine;

namespace UI.GridEquipmentNamespace
{
    public class UiInputSystem : MonoBehaviour
    {
        public static UiInputSystem instance;
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
        }
    }
}