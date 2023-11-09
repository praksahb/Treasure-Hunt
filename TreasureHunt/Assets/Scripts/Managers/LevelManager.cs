using UnityEngine.SceneManagement;

namespace TreasureHunt.MainMenu

{
    public class LevelManager
    {
        public void LoadTestLevel()
        {
            SceneManager.LoadSceneAsync((int)Level.TestLevel);
        }
    }
}
