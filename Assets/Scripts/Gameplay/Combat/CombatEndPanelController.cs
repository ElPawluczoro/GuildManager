using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Gameplay.Combat
{
    public class CombatEndPanelController : MonoBehaviour
    {
        [SerializeField] private TMP_Text resultTextComponent;
        public void LoadPanel(string resultText)
        {
            resultTextComponent.text = resultText;
        }

        public void ContinueButton()
        {
            GetComponent<LoadGameplayScene>().LoadScene();
        }
    }
}