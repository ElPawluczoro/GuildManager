using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    [CreateAssetMenu(fileName = "Level", menuName = "Level", order = 0)]
    public class Level : ScriptableObject
    {
        [SerializeField] private List<GameObject> possibleEnemies;
        [SerializeField] private int minEnemiesPerPack;
        [SerializeField] private int maxEnemiesPerPack;
        [SerializeField] private List<Vector3> possiblePositions;

        public List<GameObject> PossibleEnemies => possibleEnemies;

        public int MinEnemiesPerPack => minEnemiesPerPack;

        public int MaxEnemiesPerPack => maxEnemiesPerPack;
        
        public List<Vector3> PossiblePositions => possiblePositions;
    }
}