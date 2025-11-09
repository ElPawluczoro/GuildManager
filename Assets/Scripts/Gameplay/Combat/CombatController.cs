using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.Combat
{
    public class CombatController : MonoBehaviour
    {
        [SerializeField] private GameObject combatEndCanvas;
        public void StartFight()
        {
            foreach (FightingScript fighter in FindObjectsByType<FightingScript>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                fighter.StartFighting();
            }
        }

        public void CheckResult()
        {
            List<FightingScript> characters =
                FindObjectsByType<FightingScript>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
            List<FightingScript> player = new List<FightingScript>();
            List<FightingScript> enemy = new List<FightingScript>();
            foreach (FightingScript character in characters)
            {
                if (character.alive)
                {  
                    if(character.Side == Side.PLAYER) player.Add(character);
                    else enemy.Add(character);
                }
            }

            if (enemy.Count <= 0)
            {
                EndCombat(true);
                return;
            }
            
            if (player.Count <= 0)
            {
                EndCombat(false);
                return;
            }
        }

        private void EndCombat(bool playerWon)
        {
            combatEndCanvas.SetActive(true);
            combatEndCanvas.GetComponent<CombatEndPanelController>().LoadPanel(playerWon ? "Victory" : "Defeat");
        }
    }
}