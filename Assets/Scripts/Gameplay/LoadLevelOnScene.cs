using System;
using UnityEngine;

namespace Gameplay
{
    public class LoadLevelOnScene : MonoBehaviour
    {
        private Level currentLevel;

        public static LoadLevelOnScene Instance { get; private set; }

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
        
        /*private void Start()
        {
            Load(FindFirstObjectByType<LevelInformation>().currentLevel);
        }*/

        public void Load(Level level)
        {
            Debug.Log(level == currentLevel);
            if (level == currentLevel)
            {
                FindFirstObjectByType<CurrentLevelInformation>().LoadLevel();
            }
            else
            {
                FindFirstObjectByType<PrepareLevel>().Generate(level);
                currentLevel = level;
            }
        }
    }
}