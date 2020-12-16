using System.Collections.Generic;
using DefaultNamespace;
using Mapbox.Unity.Map;
using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using static UnityEngine.Debug;

/// <summary>
    /// Listens for touch events and performs an AR raycast from the screen touch point.
    /// AR raycasts will only hit detected trackables like feature points and planes.
    ///
    /// If a raycast hits a trackable, the <see cref="placedPrefab"/> is instantiated
    /// and moved to the hit position.
    /// </summary>
    /// 
    [RequireComponent(typeof(ARRaycastManager))]
    public class SpawnMarkerOnMap : MonoBehaviour
    {
        public string[] NetworkPrefabs = new string[3];
        public GameObject[] PreviewObjects = new GameObject[3];
        private int currentObjectIndex = 0;
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        private ARRaycastManager m_RaycastManager;
        private bool IsSelectingMarker;
        public float findingSquareDist = 0.5f;
        public GameObject spawnPoint;
        public InputField MarkerIDInput;
        public AbstractMap abstractMap;


        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            if (Input.GetMouseButtonDown(0))
            {
                touchPosition = Input.mousePosition;
                return true;

            }

            touchPosition = default;
            return false;
        }

        public void LoadObject(int objectIndex)
        {
            PreviewObjects[currentObjectIndex].SetActive(false);
            currentObjectIndex = objectIndex;
            IsSelectingMarker = true;
        }
        
        void Update()
        {
            if (!IsSelectingMarker || EventSystem.current.IsPointerOverGameObject() || !TryGetTouchPosition(out Vector2 touchPosition))
                return;
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, findingSquareDist);

            Ray ray = Camera.main.ScreenPointToRay(center);
            Debug.DrawRay(transform.position,Vector3.forward, Color.yellow, 20f);
            RaycastHit hit;
            
            
            //Check here.
            if (Physics.Raycast(ray, out hit, 100))
                //if (Physics.Raycast(ray, out hit, 100, 0))
            {
                PreviewObjects[currentObjectIndex].transform.position = hit.point;
                PreviewObjects[currentObjectIndex].SetActive(true);

            }
            
            else
            {
                PreviewObjects[currentObjectIndex].SetActive(false);
            }

        }

        public void InstantiateMarker()
        {
            if (!PreviewObjects[currentObjectIndex].activeSelf) 
                return;

            Vector3 position = PreviewObjects[currentObjectIndex].transform.position;
            
            var newMarker = 
            PhotonNetwork.Instantiate(NetworkPrefabs[currentObjectIndex], position, Quaternion.identity);
            newMarker.GetComponent<Marker>().SetUp(MarkerIDInput.text, abstractMap.WorldToGeoPosition(position));
            IsSelectingMarker = false;
            PreviewObjects[currentObjectIndex].SetActive(false);
        }
    }
