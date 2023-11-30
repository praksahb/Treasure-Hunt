using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.UI
{
    public class MainMenuPanel : MonoBehaviour
    {
        [SerializeField] private Button startGameBtn;
        [SerializeField] private TMPro.TMP_Dropdown graphicsOption;

        private void OnEnable()
        {
            startGameBtn.onClick.AddListener(LoadTestLevel);
            graphicsOption.onValueChanged.AddListener(ChangeGraphicsSettings);
        }

        private void OnDisable()
        {
            startGameBtn.onClick.RemoveAllListeners();
        }

        private void LoadTestLevel()
        {
            LevelLoader.Instance.LoadLevel(Level.TestLevel);
        }

        private void ChangeGraphicsSettings(int qualityIndex)
        {
            QualitySettings.SetQualityLevel(qualityIndex);
            Debug.Log(QualitySettings.renderPipeline.name);
        }
    }
}
