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
    Vector3 position;
    Quaternion rotation;
    float smoothing = 500f;

    

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
        DontDestroyOnLoad(gameObject);
        
    }
    
    //Create RPC function [PunRPC] - Data type has to be serialized
    [PunRPC]
    void RPC_SetAvatar(int whichBeaconAvatar, string userID)
    {
        //Instantiates the non-local players selected character/beacon to local player client
        BeaconAvatar = whichBeaconAvatar;
        UserID = userID;
        Debug.Log("Character PICKED!!");
        BeaconSpawner.Instance.RegisterPlayerView(this);
    }
    
    //
    public void Start()
    {
        //Checks if PV is local client - if not, function does not run
        if (photonView.IsMine)
        {
            //Calls RPC function by Name("RPC_AddCharacter"), sent to who across network ("AllBuffered"), values sent as parameters 
            photonView.RPC(nameof(RPC_SetAvatar),
                RpcTarget.AllBuffered, 
                UserInfo.instance.MySelectedAvatar, 
                UserInfo.instance.GetName());
            Debug.Log("Character checker?");
        }
        
        // LERP Smoothing Test
        //
        //
        else
        {
            StartCoroutine("UpdateData");
        }
        
    }
    
    // LERP Smoothing Test
    //
    //
    /*
    public void Update()
    {
        transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime *  smoothing);
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *  smoothing);
    }

    */
    IEnumerator UpdateData()
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, position, Time.deltaTime *  smoothing);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime *  smoothing);

            yield return null;
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
            
            // LERP Stuff
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        
        else
        {
            double x = (double)stream.ReceiveNext();
            double y = (double)stream.ReceiveNext();
            
            //LERP Stuff print ("Receiving");
            position = (Vector3)stream.ReceiveNext();
            rotation = (Quaternion)stream.ReceiveNext();
            
            this.LatitudeLongitude = new Vector2d(x,y);
            Debug.LogWarning("Serialized View Called");

        }
        
    }
  
}