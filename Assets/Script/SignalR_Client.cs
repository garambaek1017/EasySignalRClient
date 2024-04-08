using Assets.Script;
using Microsoft.AspNet.SignalR.Client;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SignalR_Client : MonoBehaviour
{
    public Text ChattingMessages;
    public Text PrivateChatMessage;

    public Text GetPlayerInfosMessage;

    string ChatMsg = string.Empty;
    string PrivateChatMsg = string.Empty;
    string PlayerInfoMsg = string.Empty;

    int Line = 0;

    public static bool IsAttack { get; set; }
    public static int Damage { get; set; }

    private void Awake()
    {
        Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
    }
    void OnGUI()
    {

    }

    void OnDestroy()
    {
        GameClient.Instance.Network.DisConnect();
        print("Script was destroyed");
    }

    void Start()
    {
        GameClient.Instance.SetNetwork();
        GameClient.Instance.Network.DisplayResult += DisplayResult;
    }

    bool bPaused = false;

    void OnApplicationPause(bool pause)
    {
        if(pause)
        {
            bPaused = true;
        }
        else
        {
            if (bPaused == true)
            {
                GameClient.Instance.Network.DisConnect();
                bPaused = false;
            }   
        }
    }

    void OnApplicationQuit()
    {
        GameClient.Instance.Network.DisConnect();
    }

    void DisplayPrivateChat(string name, string message)
    {
        if (this.PrivateChatMessage != null)
        {
            PrivateChatMsg = PrivateChatMsg + string.Format("{0} : {1} \n", name, message);
        }
    }

    void DisplayResult(string name, string message)
    {
        if (this.ChattingMessages != null)
        {
            ChatMsg = ChatMsg + string.Format("{0} : {1} \n", name, message);
            Line++;
        }
        Debug.Log(string.Format("{0} : {1}", name, message));
    }

    void DisplayPlayerInfos(string name, string message)
    {
        if(this.GetPlayerInfosMessage != null)
        {
            PlayerInfoMsg = string.Format("{0} : {1} \n", name, message);
        }
    }

    void DisplayChatting(string name, string message)
    {
        if (this.ChattingMessages != null)
        {
            ChatMsg = ChatMsg + string.Format("{0} : {1} \n", name, message);
        }
        Debug.Log(string.Format("{0} : {1}", name, message));
    }

    // Update is called once per frame
    void Update()
    {
        if (this.ChattingMessages != null)
        {
            ChattingMessages.text = ChatMsg;

            if (Line > 15)
            {
                ChatMsg = string.Empty;
                Line = 0;
            }   
        }   

        if (this.PrivateChatMessage != null)
            PrivateChatMessage.text = PrivateChatMsg;

        if (this.GetPlayerInfosMessage != null)
            GetPlayerInfosMessage.text = PlayerInfoMsg;
    }
}
