using UnityEngine;

namespace TreasureHunt.Interactions
{
    public class DoorManager : MonoBehaviour
    {
        [SerializeField] private DoorData doors;
        [SerializeField] private KeyBehaviour treasureKey;


        private void Start()
        {
            InitializeDoors();
            treasureKey.SetKeyType(KeyType.TreasureKey);
        }


        private void InitializeDoors()
        {
            for (int i = 0; i < doors.doorInfoList.Length; i++)
            {
                BaseDoorData doorInfo = doors.doorInfoList[i];
                GameObject door = Object.Instantiate(doorInfo.doorPrefab, doorInfo.spawnPosition, Quaternion.Euler(0f, doorInfo.spawnRotation.y, 0f));
                DoorBehaviour doorController = door.GetComponentInChildren<DoorBehaviour>();
                doorController.SetRequiredKey(doorInfo.requiredKey);
            }
        }
    }
}
