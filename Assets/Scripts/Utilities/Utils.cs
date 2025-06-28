using UnityEngine;

namespace Utilities
{
    public class Utils
    {
        public static int GetRandomValueFromBase(Vector2Int baseStats)
        {
            return Random.Range(baseStats.x, baseStats.y);
        }
    }
}