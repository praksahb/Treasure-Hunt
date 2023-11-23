using System.Collections;
using UnityEngine;

namespace TreasureHunt
{
    public class LevelLoader : GenericMonoSingleton<LevelLoader>
    {
        [SerializeField] private Animator transition;
        [SerializeField] private float transitionTime;

        private LevelManager levelManager;
        private static readonly int Start = Animator.StringToHash("Start");
        private WaitForSeconds waitTime;
        private Coroutine loadLevel;

        protected override void Awake()
        {
            base.Awake();
            levelManager = new LevelManager();
            waitTime = new WaitForSeconds(transitionTime);
        }

        public void LoadLevel(Level level)
        {
            if (loadLevel != null)
            {
                StopCoroutine(loadLevel);
            }
            loadLevel = StartCoroutine(LoadLevelCoroutine(level));
        }

        private IEnumerator LoadLevelCoroutine(Level level)
        {
            transition.SetBool(Start, true);

            yield return waitTime;

            levelManager.LoadLevel(level);

            transition.SetBool(Start, false);
        }
    }
}
