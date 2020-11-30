using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Mapbox.Utils;
using Mapbox.Unity.Location;


public class PlayerView : MonoBehaviourPun, IPunObservable
{
    public Vector2d LatitudeLongitude;
    public string UserID;


    

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

    Vector3 _targetPosition;

    public void Start()
    {
        BeaconSpawner.Instance.RegisterPlayerView(this);
    }

    public void OnDestroy()
    {
        BeaconSpawner.Instance.DeRegisterPlayerView(this);
    }

    void LateUpdate()
    {

        if (this.photonView.IsMine)
        {
            LatitudeLongitude = LocationProvider.CurrentLocation.LatitudeLongitude;
        }
    }

/// <summary>
/// 

   
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {

            stream.SendNext(LatitudeLongitude.x);
            stream.SendNext(LatitudeLongitude.y);
        }
        
        else
        {
            double x = (double)stream.ReceiveNext();
            double y = (double)stream.ReceiveNext();
            this.LatitudeLongitude = new Vector2d(x,y);
            Debug.LogWarning("Serialized View Called");

        }
        
    }
  
}