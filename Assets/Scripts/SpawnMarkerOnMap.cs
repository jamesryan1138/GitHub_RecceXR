using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

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
        public GameObject[] Prefabs = new GameObject[3];
        GameObject[] spawnObjects = new GameObject[3];
        private int currentObjectIndex = 0;
        static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
        private ARRaycastManager m_RaycastManager;


        void Awake()
        {
            m_RaycastManager = GetComponent<ARRaycastManager>();
        }

        bool TryGetTouchPosition(out Vector2 touchPosition)
        {
            if (Input.touchCount > 0)
            {
                touchPosition = Input.GetTouch(0).position;
                return true;
            }

            touchPosition = default;
            return false;
        }

        public void LoadObject()
        {
            spawnObjects[currentObjectIndex].SetActive(false);
            currentObjectIndex = 1;
            spawnObjects[currentObjectIndex].SetActive(true);
        }
        
        void Update()
        {
            if (!TryGetTouchPosition(out Vector2 touchPosition))
                return;

            if (m_RaycastManager.Raycast(touchPosition, s_Hits, TrackableType.PlaneWithinPolygon))
            {
                // Raycast hits are sorted by distance, so the first one
                // will be the closest hit.
                var hitPose = s_Hits[0].pose;
                if (spawnObjects[currentObjectIndex] == null)
                {
                    spawnObjects[currentObjectIndex] =
                        Instantiate(Prefabs[currentObjectIndex], hitPose.position, hitPose.rotation);
                }
                else
                {
                    spawnObjects[currentObjectIndex].transform.position = hitPose.position;
                }
                
            }
        }

    }
