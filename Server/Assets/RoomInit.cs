using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class RoomInit : MonoBehaviour {
    public Text txtConnect;

    void Awake()
    {
        PhotonNetwork.isMessageQueueRunning = true;
        GetConnectPlayerCount();
    }
    void GetConnectPlayerCount()
    {
        Room currRoom = PhotonNetwork.room;
        txtConnect.text = currRoom.playerCount.ToString() + "/" + currRoom.maxPlayers.ToString();
    }
    
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        GetConnectPlayerCount();
    }
    void OnPhotonPlayerDisconnected(PhotonPlayer outPlayer)
    {
        GetConnectPlayerCount();
    }
    public void OnClickExitRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

}
