using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.Cockpit;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public UserInfo userInfo;

    // TODO MOVE TO DIFFERENT SCRIPT FOR UI ITEMS
    public GameObject SaveButton;
    public GameObject StayActiveButton;
    
    //public GameObject JoinMapButton;
  
    // TODO MOVE TO DIFFERENT SCRIPT FOR UI ITEMS
    public GameObject ConnectedText;
    public GameObject DisconnectedText;
    
    public string RoomName; // Pull GroupID to use as Room Name.
    private string SceneName;

    private void Awake()
    {
        lobby = this; // creates the singleton, lives in main menu
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneFinishedLoading;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.RemoveCallbackTarget(this);
        SceneManager.sceneLoaded -= OnSceneFinishedLoading;
    }


    
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Entered Room");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        Debug.Log("Player Left Room");
    }
    
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to master Photon server.
        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("Player has connected to master server");
        
        Debug.Log("Player has connected to master server");
        
        SaveButton.SetActive(true); // Player is now connected to servers, JoinMapButton to start shit up!
        StayActiveButton.SetActive(true);
     
        ConnectedText.SetActive(true);
        DisconnectedText.SetActive(false);

    }

    public void OnStayActiveClicked()
    {
        Debug.Log("Save Button was clicked");
        StayActiveButton.SetActive(false);
        SaveButton.SetActive(false);
        SceneName = "ActiveUserScene";
        
        RoomName = userInfo.GetGroupID();
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOps, TypedLobby.Default);
        
    }
    
    public void OnSaveButtonClicked()
    {
        Debug.Log("Save Button was clicked");
        SceneName = "TabletopAR";
        
        StayActiveButton.SetActive(false);
        SaveButton.SetActive(false);
        

        RoomName = userInfo.GetGroupID();
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.JoinOrCreateRoom(RoomName, roomOps, TypedLobby.Default);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        Debug.Log("OnJoinRoom failed");
    }
    
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);

        PhotonNetwork.LoadLevel(SceneName);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a room but failed, there must already be a room with the same name");
    }

private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);

        if (scene.name == "TabletopAR" || scene.name == "ActiveUserScene")
        {
            CreatePlayer();
            Debug.Log("Create a Player playa!");

        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("PlayerView", Vector3.zero, Quaternion.identity);
           }

    private void OnApplicationPause(bool pauseStatus)
    {
        Debug.Log("OnApplication Paused " + pauseStatus);
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected error");
        if (cause != DisconnectCause.None)
        {
            Debug.LogErrorFormat("OnDisconnected, cause = {0}", cause);
            if (PhotonNetwork.ReconnectAndRejoin())
            {
                Debug.Log("Reconnected to Photon");
            }
            else
            {
                // TODO: GO BACK TO MAIN MENU, REQUIRES FIXING DONT DESTROY ON LOAD OBJECTS
                Debug.Log("Reconnect to Photon failed");
            }
        }
    }
    
}
