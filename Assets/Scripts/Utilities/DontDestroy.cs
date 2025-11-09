using System;
using UnityEngine;

namespace Utilities
{
    public class DontDestroy : MonoBehaviour
    {
        public static DontDestroy instance;

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
        }

        private void Start()
        {
            ApplyDontDestroyOnLoad();
        }

        public void ApplyDontDestroyOnLoad()
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}