using UnityEngine;
using UnityEngine.UI;

namespace TreasureHunt.MainMenu
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Button startGameBtn;


        private LevelManager levelManager;

        private void Awake()
        {
            levelManager = new LevelManager();
        }

        private void OnEnable()
        {
            startGameBtn.onClick.AddListener(LoadTestLevel);
        }

        private void OnDisable()
        {
            startGameBtn.onClick.RemoveAllListeners();
        }

        private void LoadTestLevel()
        {
            levelManager.LoadTestLevel();
        }
    }
}
