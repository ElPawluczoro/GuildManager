using System;
using UnityEngine;
using Utilities;

namespace UI.GridEquipmentNamespace
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

        public bool PlaceItem(ItemInUI newItem)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (TryPlaceItem(new Vector2Int(i, j), newItem)) return true;
                }
            }
            return false;
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
                //Debug.Log("Fragment of item off bounds");
                return false;
            }
            
            for (int i = position.x; i < position.x + newItem.SizeX; i++)
            {
                for (int j = position.y; j < position.y + newItem.SizeY; j++)
                {   
                    //Debug.Log("Space not free");
                    if(grid[i, j] != Guid.Empty) return false;
                }
            }
            
            for (int i = position.x; i < position.x + newItem.SizeX; i++)
            {
                for (int j = position.y; j < position.y + newItem.SizeY; j++)
                {   
                    grid[i, j] = newItem.Item.Guid;
                    //Debug.Log($"{i},{j}");
                }
            }

            newItem.currentPlacementIndex =
                Utils.TranslatePositionToIndex(new Vector2Int(position.x, position.y), grid.GetLength(0));
            newItem.currentGrid = this;

            if (onItemPlace != null)
            {
                onItemPlace.Invoke(newItem, position);
            }
            
            //Debug.Log("Completed");
            return true;
        }

        public void RemoveItem(ItemInUI newItem)
        {
            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for (int j = 0; j < grid.GetLength(1); j++)
                {
                    if (grid[i, j] == newItem.Item.Guid)
                    {
                        grid[i, j] = Guid.Empty;
                    }
                }
            }
        }
    }
}