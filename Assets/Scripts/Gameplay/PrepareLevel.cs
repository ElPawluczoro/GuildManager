using System;
using UnityEngine;
using Random = System.Random;

namespace Gameplay
{
    public class PrepareLevel : MonoBehaviour
    {
        [SerializeField] private GameObject encounterPrefab;
        
        public static PrepareLevel Instance { get; private set; }

        private void Awake() 
        { 
            if (Instance != null && Instance != this) 
            { 
                Destroy(this.gameObject); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
        
        public void Generate(Level level)
        {
            Random random = new Random();
            foreach (Vector3 r in level.PossiblePositions)
            {
                var newEncounter = Instantiate(encounterPrefab, r, Quaternion.identity).GetComponent<CombatTrigger>();
                
                int enemiesCount = random.Next(level.MinEnemiesPerPack, level.MaxEnemiesPerPack);
                for (int i = 0; i < enemiesCount; i++)
                {
                    newEncounter.AddEnemyToEncounter(GetRandomEnemy(level));   
                }//TODO save encouters to recreate them later
                
            }
        }

        public GameObject GetRandomEnemy(Level level)
        {
            Random random = new Random();
            var r = random.Next(0, level.PossibleEnemies.Count);
            return level.PossibleEnemies[r];
        }

        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}