using System;
using UnityEngine;

namespace Gameplay
{
    public class LevelInformation : MonoBehaviour
    {
        public Level currentLevel;

        public static LevelInformation Instance { get; private set; }

        private void Awake() 
        { 
            // If there is an instance, and it's not me, delete myself.
    
            if (Instance != null && Instance != this) 
            { 
                Destroy(this.gameObject); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }
        
        private void Start()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}