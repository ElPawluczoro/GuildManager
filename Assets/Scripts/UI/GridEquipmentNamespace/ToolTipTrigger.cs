using Characters.Player.CharacterItem;
using Characters.Player.CharacterItem.SEquipmentBases;
using ProjectEnums;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.GridEquipmentNamespace
{
    public class ToolTipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public void OnPointerEnter(PointerEventData eventData)
        {
            string contentText;

            string rarityColor = "<color=#353536>";;
            EquipableItem equipableItem = GetComponent<EquipableItem>();
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

            if (equipableItem.TotalDefensiveStats.x != 0) contentText += "armour " + equipableItem.TotalDefensiveStats.x + "\n"; 
            if (equipableItem.TotalDefensiveStats.y != 0) contentText += "magic resistance " + equipableItem.TotalDefensiveStats.y + "\n"; 
            if (equipableItem.TotalDefensiveStats.z != 0) contentText += "dodge " + equipableItem.TotalDefensiveStats.z + "\n"; 
            
            if (equipableItem.BlockChance != 0) contentText += "block chance + equipableItem.BlockChance" + "\n";

            if (equipableItem.AttackDamage != Vector2Int.zero)
                contentText += "attack damage " + equipableItem.AttackDamage.x + " - " + equipableItem.AttackDamage.y + "\n";

            if (equipableItem.AttackSpeed != 0) contentText += "Attack speed " + equipableItem.AttackSpeed + "\n";
            
            TooltipSystem.instance.ShowTooltip(contentText, GetComponent<RectTransform>());
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            TooltipSystem.instance.HideTooltip();
        }
    }
}