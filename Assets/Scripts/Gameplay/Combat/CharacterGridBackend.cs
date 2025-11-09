using System;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CharacterGridBackend : MonoBehaviour
    {
        private int[,] cellPositions;

        private void Awake()
        {
            var objectDragger = FindAnyObjectByType<ObjectDragger>();

            var horizontalLenght = GetGridLenght(new Vector2(objectDragger.PlayerDraggableBoundsHorizontal.x,
                objectDragger.PlayerDraggableBoundsHorizontal.y));
            var verticalLenght = GetGridLenght(new Vector2(objectDragger.PlayerDraggableBoundsVertical.x,
                objectDragger.PlayerDraggableBoundsVertical.y));
            
            cellPositions = new int [horizontalLenght, verticalLenght];
           //Debug.Log("len x: "  + cellPositions.GetLength(0) + "len y: "  + cellPositions.GetLength(1));
        }

        private Vector2Int TranslatePositionToCellPositionArray(Vector2 position, Vector2 boundsX, Vector2 boundsY)
        {
            float x = position.x - boundsX.x;
            float y = position.y - boundsY.x;

            return new Vector2Int((int)x, (int)y);

        }

        public Vector2Int TranslateCellPositionArrayToPosition(Vector2 cellPosition, Vector2 boundsX, Vector2 boundsY)
        {
            float x = cellPosition.x + boundsX.x;
            float y = cellPosition.y + boundsY.x;
            
            return new Vector2Int((int)x, (int)y);
        }

        public void PlaceInCell(Vector2 position, Vector2 boundsX, Vector2 boundsY)
        {
            var translatedPosition = TranslatePositionToCellPositionArray(position, boundsX, boundsY);
            cellPositions[translatedPosition.x, translatedPosition.y] = 1;
        }

        public void PlaceInCell(Vector2 position)
        {
            cellPositions[(int)position.x, (int)position.y] = 1;
        }

        public void RemoveFromCell(Vector2 position, Vector2 boundsX, Vector2 boundsY)
        {
            var translatedPosition = TranslatePositionToCellPositionArray(position, boundsX, boundsY);
            cellPositions[translatedPosition.x, translatedPosition.y] = 0;
        }

        public bool IsCellFree(Vector2 position, Vector2 boundsX, Vector2 boundsY)
        {
            var translatedPosition = TranslatePositionToCellPositionArray(position, boundsX, boundsY);
            Debug.Log("translated position: " + translatedPosition);
            Debug.Log(cellPositions[translatedPosition.x, translatedPosition.y]);
            Debug.Log("X lenght: " + cellPositions.GetLength(0) +  "Y lenght: " + cellPositions.GetLength(1));
            if (cellPositions[translatedPosition.x, translatedPosition.y] == 1) return false;
            return true;
        }

        public int GetGridLenght(Vector2 bounds)
        {
            int result;
            
            if (bounds is { x: < 0, y: < 0 } or { x: >= 0, y: >= 0 })
            {
                result = (int)Math.Abs(bounds.x) - (int)Math.Abs(bounds.y);
            }
            else //(bounds is { x: < 0, y: > 0 })
            {
                result = (int)Math.Abs(bounds.x) + (int)Math.Abs(bounds.y);
            }

            return result;
        }
        
    }
}



























