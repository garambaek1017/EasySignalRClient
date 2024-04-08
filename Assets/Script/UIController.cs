using Assets.Script;
using System;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 버튼에 관련된 기능 구현
/// </summary>
public class UIController : MonoBehaviour
{
    public InputField IP_ServerUrl;
    public InputField IP_Nickname;
    public InputField IP_Message;

    public Text ChattingMessages;


    public void Start()
    {
        if (IP_ServerUrl == null)
            Debug.LogError("IP serverUrl is null, check your IP");

        if (IP_Nickname == null)
            Debug.LogError(" IP Nickname is null, check your IP");

        if (IP_Message == null)
            Debug.LogError("IP_Message is null, check your IP");

        Debug.Log("Input field Setting is done");
    }

    /// <summary>
    /// 서버에 연결 
    /// </summary>
    public void Connect()
    {
        string url = IP_ServerUrl.text;
         
        if(string.IsNullOrEmpty(url) == true)
        {
            Debug.LogError("server Url is null, please check");
        }
        else
        {
            url = string.Format("http://{0}", url);

            Debug.Log("server url : " + url);
        }

        GameClient.Instance.Connect(url);
    }

    /// <summary>
    /// 유저 정보 저장 
    /// </summary>
    public void SetUserInfo()
    {
        var nickname = IP_Nickname.text;

        var accountIdx = DateTime.Now.Ticks;

        GameClient.Instance.SetGameUser((long)Math.Floor(Math.Log10(accountIdx) + 3), nickname);

        Debug.Log(string.Format("accountIdx : {0}, nickname:{1}, setting is done", accountIdx, nickname));
        
    }

    /// <summary>
    /// 메시지 전송
    /// </summary>
    public void SendChatMessage()
    {
        string message = this.IP_Message.text;
        GameClient.Instance.SendChat(message);
    }

    /// <summary>
    /// 연결 종료 
    /// </summary>
    public void Stop()
    {
        GameClient.Instance.Stop();

        Debug.Log("서버 연결 종료");
    }
}
