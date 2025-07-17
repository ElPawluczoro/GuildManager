using System;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.GridEquipment
{
    public class Grid : MonoBehaviour
    {
        [SerializeField] private byte sizeX, sizeY;
        [SerializeField] private GameObject slotPrefab;
        [SerializeField] private GameObject itemLayer;
        
        private GridBackend gridBackend;
        private byte cellSize = 25;
        
        public byte SizeX => sizeX;

        public byte SizeY => sizeY;

        public GameObject SlotPrefab => slotPrefab;

        public GridBackend GridBackend => gridBackend;

        private void Awake()
        {
            SetupGrid();
        }

        private void OnEnable()
        {
            gridBackend.onItemPlace += InterpretItemPlacement;
        }
        
        private void OnDisable()
        {
            gridBackend.onItemPlace -= InterpretItemPlacement;
        }

        private void SetupGrid()
        {
            GetComponent<GridLayoutGroup>().constraintCount = sizeX;
            for (int i = 0; i < sizeX * sizeY; i++)
            { 
                GridSlot gridSlot = Instantiate(slotPrefab, transform).GetComponent<GridSlot>();
                gridSlot.index = i;
            }

            //itemLayer.GetComponent<RectTransform>().pivot = gameObject.GetComponent<RectTransform>().pivot;
            itemLayer.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * cellSize, sizeY * cellSize);
            
            gridBackend = new GridBackend(sizeX, sizeY);
        }

        public void InterpretItemPlacement(ItemInUI item, Vector2Int position)
        {
            int index = Utils.TranslatePositionToIndex(position, sizeX);
            RectTransform slot = transform.GetChild(index).GetComponent<RectTransform>();
            RectTransform itemRect = item.GetComponent<RectTransform>();

            itemRect.SetParent(itemLayer.GetComponent<RectTransform>());

            itemRect.anchoredPosition = slot.anchoredPosition + new Vector2(
                cellSize / 2f * (item.SizeX - 1),
                -cellSize / 2f * (item.SizeY - 1)
            );
        }



    }   
}
