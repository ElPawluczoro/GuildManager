using System;
using UnityEngine;

namespace UI.GridEquipment
{
    public class GridBackend
    {
        private Guid[,] grid;
        
        public Guid[,] Grid => grid;

        public delegate void OnItemPlace(ItemInUI item, Vector2Int position);
        public event OnItemPlace onItemPlace;
        
        public GridBackend(byte gridX, byte gridY)
        {
            grid = new Guid[gridX, gridY];
        }

        public void PlaceItem(ItemInUI newItem)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (TryPlaceItem(new Vector2Int(i, j), newItem)) return;
                }
            }
        }

        public bool TryPlaceItem(Vector2Int position, ItemInUI newItem)
        {
            if (position.x >= grid.GetLength(0) || position.x < 0 || position.y >= grid.GetLength(1) || position.y < 0)
            {
                Debug.LogWarning($"something is trying to place an item off bound {position.x},{position.y}");
                return false;
            }

            if (position.x + newItem.SizeX - 1 >= grid.GetLength(0) || position.y + newItem.SizeY - 1>= grid.GetLength(1))
            {
                Debug.Log("Fragment of item off bounds");
                return false;
            }
            
            for (int i = position.x; i < position.x + newItem.SizeX; i++)
            {
                for (int j = position.y; j < position.y + newItem.SizeY; j++)
                {   
                    if(grid[i, j] != Guid.Empty) return false;
                }
            }
            
            for (int i = position.x; i < position.x + newItem.SizeX; i++)
            {
                for (int j = position.y; j < position.y + newItem.SizeY; j++)
                {   
                    grid[i, j] = newItem.Item.Guid;
                }
            }

            if (onItemPlace != null)
            {
                onItemPlace.Invoke(newItem, position);
            }
            
            return true;
        }
    }
}