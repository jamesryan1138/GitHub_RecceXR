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
    public int BeaconAvatar;


    

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

    // Set My Own UserID
    
    public void Awake()
    {
        if (this.photonView.IsMine)
        {
            GameController gameControllers = GameObject.FindObjectOfType <GameController>();
            UserID = gameControllers.userInfo.GetName();
            
            //TODO Call SetUp RPC with UserID ... Add debugs and hook up with Beacon.
        }
    }
    
    //Create RPC function [PunRPC] - Data type has to be serialized
    [PunRPC]
    void RPC_AddCharacter(int whichBeaconAvatar, string userID)
    {
        //Instantiates the non-local players selected character/beacon to local player client
        BeaconAvatar = whichBeaconAvatar;
        UserID = userID;
        Debug.Log("Character PICKED!!");
        
    }
    
    //
    public void Start()
    {
        BeaconSpawner.Instance.RegisterPlayerView(this);
        
        //Checks if PV is local client - if not, function does not run
        if (photonView.IsMine)
        {
            //Calls RPC function by Name("RPC_AddCharacter"), sent to who across network ("AllBuffered"), values sent as parameters 
            photonView.RPC("RPC_AddCharacter",
                RpcTarget.AllBuffered, 
                UserInfo.instance.MySelectedAvatar, 
                UserInfo.instance.GetName());
            Debug.Log("Character checker?");
        }
        
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