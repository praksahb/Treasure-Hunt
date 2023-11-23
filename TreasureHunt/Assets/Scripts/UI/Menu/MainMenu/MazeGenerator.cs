using System.Collections;
using TreasureHunt.MazeGeneration;
using UnityEngine;

namespace TreasureHunt.UI
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject wallPrefab;
        [SerializeField] private Material floorMaterial;
        [SerializeField] private float wallSize;
        [SerializeField] private int mazeRows = 10;
        [SerializeField] private int mazeColumns = 10;
        [Tooltip("Wait Time in between changing maze")]
        [SerializeField] private float waitTime;

        private GameObject mazeParent;
        private MazeLoader mazeGenerator;
        private int seedVal;
        private Coroutine mazeGeneration;
        private WaitForSeconds waitingTime;

        private void Awake()
        {
            mazeGenerator = new MazeLoader(mazeRows, mazeColumns, wallPrefab, floorMaterial);
            wallPrefab.transform.localScale = new Vector3(wallSize, wallSize, wallSize / 10f);
            waitingTime = new WaitForSeconds(waitTime);
            mazeGeneration = StartCoroutine(GenerateMazes());
        }

        private IEnumerator GenerateMazes()
        {
            GenerateMaze();

            yield return waitingTime;
            Destroy(mazeParent);

            if (mazeGeneration != null)
            {
                StopCoroutine(mazeGeneration);
            }
            mazeGeneration = StartCoroutine(GenerateMazes());
        }

        private void GenerateMaze()
        {
            mazeParent = new GameObject("MazeParent");
            seedVal = Random.Range(0, 10000);
            mazeGenerator.SetupMaze(mazeRows, mazeColumns, wallSize);
            mazeGenerator.CreateMaze(mazeParent.transform, seedVal);
        }
    }
}
