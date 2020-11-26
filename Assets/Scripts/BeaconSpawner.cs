using System;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BeaconSpawner : MonoBehaviour
    {
        public Beacon BeaconPrefab;
        
        private static BeaconSpawner _instance;
        public static BeaconSpawner Instance
        {
            get
            {
                return _instance;
            }

            private set
            {
                _instance = value;
            }
        }
        
        private void Awake()
        {
            if (Instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void RegisterPlayerView(PlayerView playerView)
        {
            
        }
        
        public void OnEnable()
        {

            SceneManager.sceneLoaded += OnSceneFinishedLoading;
        }

        public void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneFinishedLoading;
        }
        
        private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
        {
            Debug.Log(scene.name);
            //called when multiplayer scene is loaded
        
            if (scene.name == "TabletopAR")
            {
                InstatiateBeacons();
            }
        }

        private void InstatiateBeacons()
        {
            
        }

        private void InstatiateBeacon(PlayerView playerView)
        {
            Beacon NewBeacon = Instantiate(BeaconPrefab);
            NewBeacon.playerView = playerView;
        }
        
        public void start()
        {
            GameObject.FindObjectsOfType(typeof(PlayerView));
            
        }
    }
}