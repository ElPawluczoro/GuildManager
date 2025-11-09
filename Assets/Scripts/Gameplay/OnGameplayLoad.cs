using System;
using UnityEngine;

namespace Gameplay
{
    public class OnGameplayLoad : MonoBehaviour
    {
        private void Start()
        {
            LoadLevelOnScene.Instance.Load(FindFirstObjectByType<LevelInformation>().currentLevel);
        }
    }
}