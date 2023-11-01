using UnityEngine;

namespace TreasureHunt.MazeGeneration
{
    public class MazeCell
    {
        public bool visited;
        public GameObject northWall;
        public GameObject southWall;
        public GameObject eastWall;
        public GameObject westWall;
        public GameObject floor;
    }
}