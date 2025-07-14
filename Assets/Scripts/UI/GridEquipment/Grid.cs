using System;
using UnityEngine;
using UnityEngine.UI;

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
                Instantiate(slotPrefab, transform);   
            }

            //itemLayer.GetComponent<RectTransform>().pivot = gameObject.GetComponent<RectTransform>().pivot;
            itemLayer.GetComponent<RectTransform>().sizeDelta = new Vector2(sizeX * cellSize, sizeY * cellSize);
            
            gridBackend = new GridBackend(sizeX, sizeY);
        }

        public void InterpretItemPlacement(ItemInUI item, Vector2Int position)
        {
            /*int x = position.x * position.y;
            Transform slot = transform.GetChild(x);
            Vector3 pos = slot.position;
            pos += new Vector3(12.5f + (item.SizeX - 1) * 25, -12.5f + (item.SizeY - 1) * -25, 0);
            item.transform.position = pos;*/

            int index = position.x * position.y;
            RectTransform slot = transform.GetChild(index).GetComponent<RectTransform>();
            RectTransform itemRect = item.GetComponent<RectTransform>();

            itemRect.SetParent(itemLayer.GetComponent<RectTransform>(), false);

            itemRect.anchoredPosition = slot.anchoredPosition + new Vector2(
                cellSize / 2f * (item.SizeX - 1),
                -cellSize / 2f * (item.SizeY - 1)
            );


        }




    }   
}
