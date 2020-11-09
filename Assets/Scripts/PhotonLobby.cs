using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class PhotonLobby : MonoBehaviourPunCallbacks
{
    public static PhotonLobby lobby;
    public UserInfo userInfo;

    public GameObject SaveButton;
    public GameObject JoinMapButton;
    public GameObject CancelButton;

    public GameObject ConnectedText;
    public GameObject DisconnectedText;
    public string RoomName; // Pull GroupID to use as Room Name.

    private void Awake()
    {
        lobby = this; // creates the singleton, lives in main menu
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to master Photon server.
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("Player has connected to master server");
        SaveButton.SetActive(true); // Player is now connected to servers, JoinMapButton to start shit up!
        Debug.Log("Player has connected to master server");
        ConnectedText.SetActive(true);
        DisconnectedText.SetActive(false);
       
    }

    public void OnSaveButtonClicked()
    {
        Debug.Log("Save Button was clicked");
        SaveButton.SetActive(false);
        CancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); //Trying to join a random map.
    }

    public override void OnJoinRandomFailed(short returnCode, string message) //When JoinRandomRoom fails this is called.
    {
        Debug.Log("Tried to join a random room but failed. You have no friends");
        CreateRoom();
    }

    public void CreateRoom() //Trying to create a room that does not already exist.
    {
        // RoomName = UserInfo.; // Tried to pull from UserInfo 
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(RoomName, roomOps); // Trying to create a room with specified values.
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Holy fuck I'm in a room!" + RoomName);
        JoinMapButton.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a room but failed, there must already be a room with the same name");
        CreateRoom(); //Retrying to create a new room with different name.
    }

    public void OnCancelButtonClicked()
    {
        Debug.Log("Cancel Button was clicked");
        PhotonNetwork.LeaveRoom();
        CancelButton.SetActive(false);
        
        ConnectedText.SetActive(false);
        DisconnectedText.SetActive(true);
    }

    public void LoadScene()
    {
        PhotonNetwork.LoadLevel("TabletopAR");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
