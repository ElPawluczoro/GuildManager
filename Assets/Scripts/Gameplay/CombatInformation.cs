using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay
{
    public class CombatInformation : MonoBehaviour
    {
        [SerializeField] private List<GameObject> playerCharacters = new();
        [SerializeField] private List<GameObject> enemyCharacters = new();
        [SerializeField] private Transform enemyHolder;
        public List<GameObject> PlayerCharacters { get { return playerCharacters; } }
        public List<GameObject> EnemyCharacters { get { return enemyCharacters; } }
        
        public static CombatInformation instance;

        private void Awake()
        {
            if (instance != null && instance != this) 
            { 
                Destroy(this.gameObject); 
            } 
            else 
            { 
                instance = this; 
            } 
            
            DontDestroyOnLoad(this.gameObject);
        }
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void AddEnemy(GameObject enemy)
        {
            enemyCharacters.Add(enemy);
            //enemy.transform.SetParent(enemyHolder);
        }

        public void ClearEnemies()
        {
            enemyCharacters.Clear();
            var toDestroy = new List<GameObject>();
            foreach (Transform t in enemyHolder.transform)
            {
                toDestroy.Add(t.gameObject);
            }

            for (int i = toDestroy.Count - 1; i >= 0; i--)
            {
                Destroy(toDestroy[i]);
            }
            
            
        }
    }
}