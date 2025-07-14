using System;
using Characters.Player.CharacterItem;
using UnityEngine;

namespace UI.GridEquipment
{
    public class ItemInUI : MonoBehaviour
    {
        [SerializeField] private Item item;
        [SerializeField] private byte sizeX, sizeY;

        private byte cellSize = 25;
        public Item Item => item;

        public byte SizeX => sizeX;

        public byte SizeY => sizeY;

        private void OnEnable()
        {
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * cellSize, sizeY * cellSize);
            item = GetComponent<Item>();
        }
    }
}