using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PhotonInit : MonoBehaviour {
    public string version = "v1.0";
    // Use this for initialization
    public InputField userId;
    public InputField roomName;
    public InputField _RoomName;
    public GameObject scrollContects;
    public GameObject roomItem;

    void Awake()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(version);
        }
        userId.text = GetUserId();

        roomName.text = "Room_" + Random.Range(0, 999).ToString("000");
    }
   
    void OnJoinedLobby()
    {
        Debug.Log("Entered Lobby!");
        userId.text = GetUserId();
        //PhotonNetwork.JoinRandomRoom(); 로비 화면 시도를 위해 주석처리
    }
    
    string GetUserId()
    {
        string userId = PlayerPrefs.GetString("USER_ID");

        if(string.IsNullOrEmpty(userId))
        {
            userId = "USER_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }
    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("No rooms!");
        //PhotonNetwork.CreateRoom("Myroom");
    }
    void OnJoinedRoom()
    {
        Debug.Log("Enter Room : " + roomName);
        StartCoroutine(this.LoadRoom());
    }
    IEnumerator LoadRoom()
    {
        PhotonNetwork.isMessageQueueRunning = false;
        AsyncOperation ao = SceneManager.LoadSceneAsync("LoadRoom");
        yield return ao;
    }
    public void OnClickJoinRandomRoom()
    {
        PhotonNetwork.player.name = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        PhotonNetwork.JoinRandomRoom();
    }
    public void OnClickCreateRoom()
    {
        string _roomName = roomName.text;
        if(string.IsNullOrEmpty(roomName.text))
        {
            _roomName = "Room_" + Random.Range(0, 999).ToString("000");
        }
        PhotonNetwork.player.name = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.isOpen = true;
        roomOptions.isVisible = true;
        roomOptions.maxPlayers = 8;

        PhotonNetwork.CreateRoom(_roomName, roomOptions, TypedLobby.Default);
    }
    void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        //Debug.Log("No rooms!");
        Debug.Log("Create Room Failed = " + codeAndMsg[1]);
        //PhotonNetwork.CreateRoom(roomName.text);
    }

    void OnReceivedRoomListUpdate()
    {
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ROOM_ITEM"))
        {
            Destroy(obj);
        }
        int rowCount = 0;
        scrollContects.GetComponent<RectTransform>().sizeDelta = Vector2.zero;
        foreach(RoomInfo _room in PhotonNetwork.GetRoomList())
        {
            Debug.Log(_room.name);
            GameObject room = (GameObject)Instantiate(roomItem);
            room.transform.SetParent(scrollContects.transform, false);

            RoomData roomData = room.GetComponent<RoomData>();
            roomData.roomName = _room.name;
            roomData.connectPlayer = _room.playerCount;
            roomData.maxPlayers = _room.maxPlayers;
            roomData.DispRoomData();
            roomData.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { OnClickRoomItem(roomData.roomName); });

            scrollContects.GetComponent<GridLayoutGroup>().constraintCount = ++rowCount;
            scrollContects.GetComponent<RectTransform>().sizeDelta += new Vector2(0,20);
        }
    }
    void OnClickRoomItem(string roomName)
    {
        PhotonNetwork.player.name = userId.text;
        PlayerPrefs.SetString("USER_ID", userId.text);
        PhotonNetwork.JoinRoom(roomName);
        Debug.Log("Enter Room : " + roomName);

    }
    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
    }
}
