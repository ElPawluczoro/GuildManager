using System;
using UnityEngine;


namespace Gameplay.Combat
{
    public class ObjectDragger : MonoBehaviour
    {
        private Transform draggable;
        private bool dragging = false;

        [SerializeField] private Vector2 playerDraggableBoundsVertical;
        [SerializeField] private Vector2 playerDraggableBoundsHorizontal;
        
        private Grid grid;
        private Camera cam;

        [SerializeField] private GameObject gridVisualisation;

        public Vector2 PlayerDraggableBoundsVertical => playerDraggableBoundsVertical;

        public Vector2 PlayerDraggableBoundsHorizontal => playerDraggableBoundsHorizontal;

        private void Awake()
        {
            grid = FindAnyObjectByType<Grid>();
            cam = Camera.main;
        }

        private void Start()
        {
            var draggableUnits =
                FindObjectsByType<DraggableUnit>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);
            
            var characterGridBackend = grid.GetComponent<CharacterGridBackend>();
            
            for (int i = 0; i < draggableUnits.Length; i++)
            {
                var unit = draggableUnits[i];
                var gridBackendCellPositon = new Vector2(0, i);
                Vector2Int position = characterGridBackend.TranslateCellPositionArrayToPosition(gridBackendCellPositon, playerDraggableBoundsHorizontal, playerDraggableBoundsVertical);
                Vector3Int cellPosition = new Vector3Int(position.x, position.y, 0);
                unit.transform.position = grid.GetCellCenterWorld(cellPosition);
                characterGridBackend.PlaceInCell(gridBackendCellPositon);
            }

        }

        private void Update()
        {
            if (dragging)
            {
                var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
                draggable.transform.position =
                    new Vector3(mousePosition.x, mousePosition.y, draggable.transform.position.z);
                if(Input.GetMouseButtonUp(0)) StopDrag();
                return;
            }
            
            Vector2 origin = cam.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(origin, Vector2.zero);
            if (hit.collider == null) return;
            if (hit.collider.CompareTag("Draggable") && Input.GetMouseButtonDown(0))
            {
                Drag(hit.transform);
            }
        }

        public void Drag(Transform target)
        {
            target.GetComponent<DraggableUnit>().lastPosition = grid.WorldToCell(target.position);
            gridVisualisation.SetActive(true);
            dragging = true;
            draggable = target;
        }

        public void StopDrag()
        {
            Vector3Int cell = grid.WorldToCell(draggable.transform.position);
            var characterGridBackend = grid.transform.GetComponent<CharacterGridBackend>();
            var lastPosition = draggable.GetComponent<DraggableUnit>().lastPosition;

            Debug.Log(new Vector2(cell.x, cell.y));
            
            if (IsInBounds(cell) && characterGridBackend.IsCellFree
                    (new Vector2(cell.x, cell.y), 
                        playerDraggableBoundsHorizontal, playerDraggableBoundsVertical))
            {
                draggable.transform.position = grid.GetCellCenterWorld(cell);
                characterGridBackend.PlaceInCell(new Vector2(cell.x, cell.y),
                    playerDraggableBoundsHorizontal, playerDraggableBoundsVertical);
                
                characterGridBackend.RemoveFromCell(new Vector2(lastPosition.x, lastPosition.y),
                    playerDraggableBoundsHorizontal, playerDraggableBoundsVertical);
            }
            else
            {
                draggable.transform.position = grid.GetCellCenterWorld(lastPosition);
            }
            
            /*Debug.Log("x: " + cell.x);
            Debug.Log("y: " + cell.y);*/
            dragging = false;
            draggable = null;
            gridVisualisation.SetActive(false);
        }

        public bool IsInBounds(Vector3Int cellPosition)
        {
            if (cellPosition.x >= playerDraggableBoundsHorizontal.y) return false;
            if (cellPosition.x < playerDraggableBoundsHorizontal.x) return false;
            if (cellPosition.y >= playerDraggableBoundsVertical.y) return false;
            if (cellPosition.y < playerDraggableBoundsVertical.x) return false;
            return true;
        }
        
    }
}