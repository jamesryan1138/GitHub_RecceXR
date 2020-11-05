using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class PhotonNetworkConnection : MonoBehaviour

{
    public string VersionName = "0.1";
    public GameObject ConnectedText;
    public GameObject DisconnectedText;

    public void ConnectToPhoton()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting to Photon");
    }
    private void ConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(typedLobby: default);
        Debug.Log("Connected to Master");
    }
    private void OnJoinLobby()
    {
        ConnectedText.SetActive(true);
        DisconnectedText.SetActive(false);
        Debug.Log("Joined Lobby");

    }
    private void OnDisconnectedFromPhoton()
    {
        if (ConnectedText)
        {
            ConnectedText.SetActive(false);
            DisconnectedText.SetActive(true);
        }
        Debug.Log("Disconnect from Photon");
    }

}
