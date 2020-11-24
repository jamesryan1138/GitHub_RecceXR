using UnityEngine;

namespace DefaultNamespace
{
    public class BeaconSpawner
    {
        public void start()
        {
            GameObject.FindObjectsOfType(typeof(PlayerView));
        }
    }
}