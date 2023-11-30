using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class DoorManager : MonoBehaviour
    {
        [SerializeField] private KeyBehaviour treasureKey;

        private DoorData doors;

        private void Start()
        {
            doors = Resources.Load<DoorData>("ScriptableObjects/DoorData");
            InitializeDoors();
            treasureKey.SetKeyType(KeyType.TreasureKey);
        }


        private void InitializeDoors()
        {
            for (int i = 0; i < doors.doorInfoList.Length; i++)
            {
                BaseDoorData doorInfo = doors.doorInfoList[i];
                GameObject door = Object.Instantiate(doorInfo.doorPrefab, doorInfo.spawnPosition, Quaternion.Euler(0f, doorInfo.spawnRotationAngle, 0f));
                door.transform.localScale = doorInfo.localScaleValues;
                door.GetComponentInChildren<DoorBehaviour>().SetDoorLockState = doors.lockState;
                door.name = doorInfo.doorName;
                DoorBehaviour doorController = door.GetComponentInChildren<DoorBehaviour>();
                doorController.SetRequiredKey(doorInfo.requiredKey);
            }
        }
    }
}
