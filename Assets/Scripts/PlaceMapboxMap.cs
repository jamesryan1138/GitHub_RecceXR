﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;
using Mapbox.CheapRulerCs;
using Mapbox.Utils;
using UnityARInterface;

public class PlaceMapboxMap : MonoBehaviour
{
    private bool MapPlaced;
    //for editor version
    
    public GameObject focusSquare;
    
    public float maxRayDistance = 30.0f;
    public LayerMask collisionLayerMask;
    public float findingSquareDist = 0.5f;
    public Transform _mapTransform;
    public AbstractMap _map;



    //public string CentreMeX;
    //public string CentreMeY;
    //public  Vector2d CentreMeXY;

    ///  
    ///

    /// </summary>
    /// <value>The current location.</value>
    ///

    protected Location _currentLocation;
    public Location CurrentLocation
    {
        get
        {
            return _currentLocation;
        }
    }


    ILocationProvider _locationProvider;
    ILocationProvider LocationProvider

    {
        get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!MapPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            InstantiateMap();

        }

        if (!MapPlaced && Input.GetMouseButtonDown(0))
        {
            InstantiateMap();

        }
        
    }

    void InstantiateMap()
    {
        Debug.Log("Instantiate Map");

        //use center of screen for focusing
        Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, findingSquareDist);

        Ray ray = Camera.main.ScreenPointToRay(center);
        RaycastHit hit;

        //we'll try to hit one of the plane collider gameobjects that were generated by the plugin
        //effectively similar to calling HitTest with ARHitTestResultType.ARHitTestResultTypeExistingPlaneUsingExtent
        if (Physics.Raycast(ray, out hit, maxRayDistance, collisionLayerMask))
        {
            _mapTransform.gameObject.SetActive(true);
            _mapTransform.position = hit.point;
            MapPlaced = true;

            // Turn off Focus Square!!
            focusSquare.SetActive(false);
            
            


        }

    }
}
