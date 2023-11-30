using UnityEngine;

namespace TreasureHunt.Interactions
{
    [CreateAssetMenu(fileName = "DoorData", menuName = "ScriptableObjects/DoorData")]
    public class DoorData : ScriptableObject
    {
        public bool lockState;
        public BaseDoorData[] doorInfoList;
    }

    [System.Serializable]
    public class BaseDoorData
    {
        public string doorName;
        public KeyType requiredKey;
        public GameObject doorPrefab;
        public Vector3 spawnPosition;
        public Vector3 localScaleValues;
        public float spawnRotationAngle;
    }
}
