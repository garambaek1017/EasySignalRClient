using Assets.Script;
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 버튼에 관련된 기능 구현
/// </summary>
public class ButtonController : MonoBehaviour
{
    public InputField txt_IP;
    public InputField txt_Port;
    public InputField txt_NickName;
    public InputField txt_Message;

    /*public InputField txt_JoinRoomId;
    public InputField txt_targetIdx;
    public InputField txt_privateChatMsg;*/

    public Text ChattingMessages;

    public void Start()
    {
        if(txt_IP == null)
            Debug.LogError("check your txt_ip obj");
        if (txt_Port == null)
            Debug.LogError("check your txt_port obj");
        if (txt_NickName == null)
            Debug.LogError("check your txt_nickname obj");
        if (txt_Message == null)
            Debug.LogError("check your txt_message obj");
    }

    public void Connect()
    {
        if (string.IsNullOrEmpty(txt_IP.text) == true)
        {
            Debug.LogError("check your ip addr..");
            return;
        }

        if (string.IsNullOrEmpty(txt_Port.text) == true)
        {
            Debug.LogError("check your port number");
            return;
        }

        var url = string.Format("http://{0}:{1}/", txt_IP.text, txt_Port.text);

        Debug.Log($"try connect -> {url}");

        GameClient.Instance.Connect(url);
    }

    public void SaveNickname()
    {
        var nickName = txt_NickName.text;

        var accountIdx = DateTime.Now.Ticks;

        GameClient.Instance.SetGameUser((long)Math.Floor(Math.Log10(accountIdx) + 3), nickName);

        Debug.Log(string.Format("accountIdx -> {0}, nickname -> {1}, setting is done", accountIdx, nickName));
    }


    public void BroadCast()
    {
        var message = txt_Message.text;

        GameClient.Instance.SendChat(message);
    }

    public void Stop()
    {
        GameClient.Instance.Stop();
        Debug.Log("Stop button click");
    }
}
