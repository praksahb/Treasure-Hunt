using UnityEngine.SceneManagement;

namespace TreasureHunt.MainMenu

{
    public class LevelManager
    {
        public void LoadTestLevel()
        {
            SceneManager.LoadSceneAsync((int)Level.TestLevel);
        }

        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync((int)Level.MainMenu);
        }

        public void RestartLevel()
        {
            int activeIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(activeIndex);
        }
    }
}
