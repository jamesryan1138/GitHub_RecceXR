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

    public GameObject SaveButton;
    public GameObject StayActiveButton;
    //public GameObject JoinMapButton;
  

    public GameObject ConnectedText;
    public GameObject DisconnectedText;
    public string RoomName; // Pull GroupID to use as Room Name.
    private string SceneName;

    private void Awake()
    {
        lobby = this; // creates the singleton, lives in main menu
        // #Critical
        // this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    //Enables Scene Loads
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


    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Connects to master Photon server.
        DontDestroyOnLoad(gameObject);
    }

    public override void OnConnectedToMaster()
    {
        //Debug.Log("Player has connected to master server");
        SaveButton.SetActive(true); // Player is now connected to servers, JoinMapButton to start shit up!
        StayActiveButton.SetActive(true);
        Debug.Log("Player has connected to master server");
        ConnectedText.SetActive(true);
        DisconnectedText.SetActive(false);
        PhotonNetwork.AutomaticallySyncScene = true;
       
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
        Debug.Log("Room create failed");
    }

    /*
     * public override void OnJoinRandomFailed(short returnCode, string message) //When JoinRandomRoom fails this is called.
     
    {
        Debug.Log("Tried to join a random room but failed. You have no friends");
        CreateRoom();
    }

    public void CreateRoom() //Trying to create a room that does not already exist.
    {
        RoomName = userInfo.GroupIDInput; // Tried to pull from UserInfo 
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 10 };
        PhotonNetwork.CreateRoom(RoomName, roomOps); // Trying to create a room with specified values.
    }
    
    */
// NEW CODE ADDED FOR SCENE SYNC BETWEEN CLIENTS
//
/// 
/// /////////////////////
/// 
    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom);
        //JoinMapButton.SetActive(true);
        //if (!PhotonNetwork.IsMasterClient)
        //Debug.Log("Master Client Check");
        //PhotonNetwork.AutomaticallySyncScene = true;
       // Debug.Log("Sync Scene Check");
        PhotonNetwork.LoadLevel(SceneName);
    }


    
//
//
/// /////////////////////
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Tried to create a room but failed, there must already be a room with the same name");
        //CreateRoom(); //Retrying to create a new room with different name.
    }
/*
public void OnCancelButtonClicked()
{
    Debug.Log("Cancel Button was clicked");
    PhotonNetwork.LeaveRoom();
    CancelButton.SetActive(false);
    
    ConnectedText.SetActive(false);
    DisconnectedText.SetActive(true);
}


 * public void LoadScene()
 
{
    //PhotonNetwork.Instantiate("Player", Vector3.zero, Quaternion.identity);
    PhotonNetwork.LoadLevel("TabletopAR");
}
*/
// PASTED IN FROM PHOTON PROJECT
/*
    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        //photonPlayers = PhotonNetwork.PlayerList;
        //playersInRoom = photonPlayers.Length;
        //myNumberInRoom = playersinRoom;

        Debug.Log("We are in a room");
        if (!PhotonNetwork.IsMasterClient)
            return;
        StartGame();

    }

    void StartGame()
    {
        //loads multiplayer scene for all players
        //isGameLoaded = true;
        if (!PhotonNetwork.IsMasterClient)
            return;
        PhotonNetwork.LoadLevel(1);
    }
    */
    //
    


    private void OnSceneFinishedLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log(scene.name);
        //called when multiplayer scene is loaded
        
        if (scene.name == "TabletopAR" || scene.name == "ActiveUserScene")
        {
            CreatePlayer();
            Debug.Log("Create a Player playa!");
            //PhotonNetwork.AutomaticallySyncScene = true;
            Debug.Log("SYNC SCENE AGAIN??");
        }
    }

    private void CreatePlayer()
    {
        PhotonNetwork.Instantiate("PlayerView", Vector3.zero, Quaternion.identity);
        //PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    }

}
