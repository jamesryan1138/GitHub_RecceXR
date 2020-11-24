using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Mapbox.Utils;
using Mapbox.Unity.Location;


public class PlayerView : MonoBehaviourPun, IPunObservable
{
    public Vector2d LatitudeLongitude;
    public string UserID;


    bool _isInitialized;

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

    void Start()
    {
        LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
    }

    void LateUpdate()
    {

        if (this.photonView.IsMine && _isInitialized)
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