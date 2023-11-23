using UnityEngine.SceneManagement;

namespace TreasureHunt

{
    public class LevelManager
    {
        public void LoadLevel(Level level)
        {
            // temp fix
            if (level == Level.Restart)
            {
                level = Level.TestLevel;
            }
            SceneManager.LoadSceneAsync((int)level);
        }
    }
}
