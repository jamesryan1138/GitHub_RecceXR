using System;
using System.Collections.Generic;
using System.Linq;
using Mapbox.Unity.Location;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class BeaconSpawner : MonoBehaviour
    {
        public Beacon BeaconPrefab;

        public Dictionary<PlayerView, Beacon> ViewToBeacon;
        public bool IsInMapScene;
        
        
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
            if (IsInMapScene)
            {
                ViewToBeacon[playerView] = InstantiateBeacon(playerView);
            }
            else
            {
                ViewToBeacon[playerView] = null;
            }
        }

        public void DeRegisterPlayerView(PlayerView playerView)
        {
            Beacon beacon = ViewToBeacon[playerView];
            if (beacon != null)
            {
                Destroy(beacon.gameObject);
            }

            ViewToBeacon.Remove(playerView);

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
                InstantiateBeacons();
                IsInMapScene = true;
            }
            else
            {
                IsInMapScene = false;
            }
        }

        private void InstantiateBeacons()
        {
            foreach (var key in ViewToBeacon.Keys.ToList())
            {
                Beacon value = ViewToBeacon[key];
                if (value == null)
                {
                    ViewToBeacon[key] = InstantiateBeacon(key);
                }
            }
        }

        private Beacon InstantiateBeacon(PlayerView playerView)
        {
            Beacon NewBeacon = Instantiate(BeaconPrefab);
            NewBeacon.playerView = playerView;

            return NewBeacon;
        }
        
        public void Start()
        {
            ViewToBeacon = new Dictionary<PlayerView, Beacon>();
            PlayerView[] playerViews = GameObject.FindObjectsOfType <PlayerView>();
            foreach (var playerView in playerViews)
            {
                ViewToBeacon[playerView] = null;
            }
            
        }
    }
}