using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class CombatTrigger : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemies = new();

        public void AddEnemyToEncounter(GameObject enemy)
        {
            enemies.Add(enemy);
            Instantiate(enemy, transform);
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            CombatInformation combatInformation =
                GameObject.FindGameObjectWithTag("CombatInformation").GetComponent<CombatInformation>();
            
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                combatInformation.AddEnemy(enemies[i]);
            }

            //yield return new WaitForSeconds(1);
            
            if (other.CompareTag("Player"))
            {
                FindFirstObjectByType<CurrentLevelInformation>().SaveLevel(this.gameObject);
                SceneManager.LoadScene(2);
            }
        }
    }
}