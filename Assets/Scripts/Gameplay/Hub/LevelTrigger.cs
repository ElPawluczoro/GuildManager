using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Hub
{
    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] Level level;
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                FindFirstObjectByType<LevelInformation>().currentLevel = level;
                SceneManager.LoadScene(1);
            }
        }
    }
}