using System;
using Characters.Player.CharacterItem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.GridEquipment
{
    public class ItemInUI : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private byte sizeX, sizeY;

        public int currentPlacementIndex;
        public GridBackend currentGrid;
        
        private byte cellSize = 25;
        public Item Item => item;

        public byte SizeX => sizeX;

        public byte SizeY => sizeY;

        public void SetSize(byte x, byte y)
        {
            sizeX = x;
            sizeY = y;
        }
        
        public void UpdateSize()
        {
            var rectTransform = gameObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(sizeX * cellSize, sizeY * cellSize);
            item = GetComponent<Item>();
            GetComponent<UiDragItem>().SetTopLeftPoint(rectTransform.sizeDelta);
        }

        public void SetImage(Sprite sprite)
        {
            GetComponent<Image>().sprite = sprite;
        }
    }
}