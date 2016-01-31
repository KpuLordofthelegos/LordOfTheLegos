using UnityEngine;
using System.Collections.Generic;

public class Network_Chat : Photon.MonoBehaviour
{
    public static Network_Chat SP;
    public List<string> messages = new List<string>();

    private int chatHeight = (int)140;
    private Vector2 scrollPos = Vector2.zero;
    private string chatInput = "";

    void Awake()
    {
        SP = this;
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, Screen.height - chatHeight, Screen.width, chatHeight));

        scrollPos = GUILayout.BeginScrollView(scrollPos);
        GUI.color = Color.black;
        for (int i = messages.Count - 1; i >= 0; i--)
        {
            GUILayout.Label(messages[i]);
        }
        GUILayout.EndScrollView();
        GUI.color = Color.white;
        chatInput = GUILayout.TextField(chatInput);

        GUILayout.BeginHorizontal();
        GUI.color = Color.black;
        GUILayout.Label("Send to:", GUILayout.Width(60));
        GUI.color = Color.white;
        if (GUILayout.Button("ALL", GUILayout.Height(17)))
            SendChat(PhotonTargets.All);
        if (GUILayout.Button("ALLBUF", GUILayout.Height(17)))
            SendChat(PhotonTargets.AllBuffered);
        if (GUILayout.Button("OTHER", GUILayout.Height(17)))
            SendChat(PhotonTargets.Others);
        if (GUILayout.Button("OTHERBUF", GUILayout.Height(17)))
            SendChat(PhotonTargets.OthersBuffered);
        if (GUILayout.Button("MASTER", GUILayout.Height(17)))
            SendChat(PhotonTargets.MasterClient);
        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    public static void AddMessage(string text)
    {
        SP.messages.Add(text);
        if (SP.messages.Count > 15)
            SP.messages.RemoveAt(0);
    }
    void SendChat(PhotonTargets target)
    {
        photonView.RPC("SendChatMessage", target, chatInput);
        chatInput = "";
    }

    [PunRPC]
    void SendChatMessage(string text, PhotonMessageInfo info)
    {
        AddMessage("[" + info.sender + "]" + text);
    }
    // Use this for initialization
}
