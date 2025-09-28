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
        
        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void AddEnemy(GameObject enemy)
        {
            enemyCharacters.Add(enemy);
            enemy.transform.SetParent(enemyHolder);
        }
    }
}