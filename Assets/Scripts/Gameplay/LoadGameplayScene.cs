using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay
{
    public class LoadGameplayScene : MonoBehaviour
    {
        public void LoadScene()
        {
            CombatInformation combatInformation = FindFirstObjectByType<CombatInformation>();
            combatInformation.ClearEnemies();
            
            SceneManager.LoadScene(1);
        }
        
    }
}