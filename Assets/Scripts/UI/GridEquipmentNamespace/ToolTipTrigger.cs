using System;
using System.Collections.Generic;
using Characters.Player.CharacterItem;
using Characters.Player.CharacterItem.SEquipmentBases;
using ProjectEnums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Utilities;

namespace UI.GridEquipmentNamespace
{
    public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            ShowTooltip();
        }

        public void ShowTooltip()
        {
            string contentText;

            string rarityColor = "<color=#353536>";;
            EquipableItem equipableItem = GetComponent<EquipableHolder>().EquipableItem;
            SEquipmentBase equipmentBase = equipableItem.equipmentBase;
            
            if (equipableItem.Rarity == Rarity.COMMON) rarityColor = "<color=#353536>"; // gray
            else if (equipableItem.Rarity == Rarity.MAGIC) rarityColor = "<color=#1c1cc9>"; // blue
            else if (equipableItem.Rarity == Rarity.RARE) rarityColor = "<color=#c49d1b>"; // yellow

            contentText = rarityColor + equipmentBase.BaseName + "</color>\n";
            
            if(equipableItem.StatRequirements.x != 0) contentText += "<color=#d41c1c> str " + equipableItem.StatRequirements.x + "</color>";
            if(equipableItem.StatRequirements.y != 0) contentText += "<color=#102de6> int " + equipableItem.StatRequirements.y + "</color>";
            if(equipableItem.StatRequirements.z != 0) contentText += "<color=#1ead17> dex " + equipableItem.StatRequirements.z + "</color>";
            contentText += "\n";

            contentText += "required lvl " + equipableItem.LevelRequirement + "\n";

            SArmourBase armorBase = equipableItem.equipmentBase as SArmourBase;
            if (equipableItem.TotalDefensiveStats.x != 0)
            {
                contentText += "armour " + equipableItem.TotalDefensiveStats.x + 
                    (UiInputSystem.instance.altHeld && armorBase ? $" ({armorBase.Armour.x} - {armorBase.Armour.y})" : "") + "\n";
            }
            if (equipableItem.TotalDefensiveStats.y != 0)
            {
                contentText += "magic resistance " + equipableItem.TotalDefensiveStats.y  +
                    (UiInputSystem.instance.altHeld && armorBase ? $" ({armorBase.MagicResistance.x} - {armorBase.MagicResistance.y})" : "") + "\n";
            } 
            if (equipableItem.TotalDefensiveStats.z != 0) {
                contentText += "dodge " + equipableItem.TotalDefensiveStats.z + 
                    (UiInputSystem.instance.altHeld && armorBase? $" ({armorBase.Dodge.x} - {armorBase.Dodge.y})" : "") + "\n";
            }
            
            SShieldBase shieldBase = equipableItem.equipmentBase as SShieldBase;
            if (equipableItem.BlockChance != 0)
            {
                contentText += "block chance" + equipableItem.BlockChance  + 
                    (UiInputSystem.instance.altHeld && shieldBase ? $" ({shieldBase.BlockChance.x} - {shieldBase.BlockChance.y})" : "") + "\n";
            }

            SWeaponBase weaponBase = equipableItem.equipmentBase as SWeaponBase;
            if (equipableItem.AttackDamage != Vector2Int.zero)
                contentText += "attack damage " + equipableItem.AttackDamage.x + " - " + equipableItem.AttackDamage.y 
                    +(UiInputSystem.instance.altHeld && weaponBase ? 
                    $"(min {weaponBase.MinDamage.x} - {weaponBase.MinDamage.y} " +
                    $"max {weaponBase.MinDamage.x + weaponBase.MaxDamageAdd} - {weaponBase.MinDamage.y + weaponBase.MaxDamageAdd})" : "") + "\n";

            if (equipableItem.AttackSpeed != 0) contentText += "Attack speed " + equipableItem.AttackSpeed + "\n";

            contentText += GetAffixString(equipableItem.Prefixes, true);
            contentText += GetAffixString(equipableItem.Suffixes, false);
            
            
            TooltipSystem.instance.ShowTooltip(contentText, GetComponent<RectTransform>(), this);
        }

        private string GetAffixString(List<Affix> affixList, bool prefix)
        {
            
            string contentText = "";
            AffixGenerator affixGenerator = FindAnyObjectByType<AffixGenerator>();
            
            if (affixList.Count <= 0) return "";
            

            /*foreach (Affix affix in affixList)
            {
                string[] affixString = Utils.AffixToString(affix.Affix1);
                contentText += $"[{affix.Tier}] " + affixString[0] + $" {affix.Value}" + affixString[1] + 
                        (UiInputSystem.instance.altHeld ? 
                            (prefix ?  $" {AffixGenerator.prefixes[affix.Affix1][affix.Tier].x} - {AffixGenerator.prefixes[affix.Affix1][affix.Tier].y}"
                            : $"{AffixGenerator.suffixes[affix.Affix1][affix.Tier].x} - {AffixGenerator.suffixes[affix.Affix1][affix.Tier].y}") : "")
                        + "\n";
            }*/
            
            foreach (Affix affix in affixList)
            {
                string[] affixString = Utils.AffixToString(affix.Affix1);
                string valueRange = "";
                if (UiInputSystem.instance.altHeld)
                {
                    Vector2Int range = prefix
                        ? AffixGenerator.prefixes[affix.Affix1][affix.Tier]
                        : AffixGenerator.suffixes[affix.Affix1][affix.Tier];
                    
                    valueRange = $"({range.x} - {range.y})";
                }

                contentText += $"[{affix.Tier}] {affixString[0]} {affix.Value}{affixString[1]} {valueRange} \n";
            }

            return contentText;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.instance.HideTooltip();
        }
    }
}