using UnityEngine;

namespace TreasureHunt
{
    public class CameraRotation : MonoBehaviour
    {
        public float speed;

        // Update is called once per frame
        void Update()
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
    }
}
