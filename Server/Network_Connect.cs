using UnityEngine;
using System.Collections;

public class Network_Connect : Photon.MonoBehaviour {

    // Use this for initialization
    void Start() {
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    void OnJoinedLobby()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        RoomOptions ro = new RoomOptions();
        ro.isVisible = true;
        ro.isOpen = true;
        ro.maxPlayers = 8;
        ro.customRoomProperties = new ExitGames.Client.Photon.Hashtable() { { "", 0 } };
        PhotonNetwork.CreateRoom("", ro, TypedLobby.Default);
    }
    void OnJoinedRoom()
    {
        Debug.Log("OK!");
    }
	// Update is called once per frame
	void Update () {
	
	}
}
