using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PunSampleUI : MonoBehaviour
{
    [SerializeField]
    private PunSampleManager pun;

    [Header("Create/Join room")]
    public CanvasGroup RoomSetting;

    [SerializeField]
    private Button createRoomBt;

    [SerializeField]
    private Button joinRoomBt;

    [SerializeField]
    private InputField roomNameInput;

    [SerializeField]
    private InputField roomSizeInput;

    [Header("Chat room")]
    public CanvasGroup ChatSetting;

    [SerializeField]
    private Text msgDisplayBoard;

    [SerializeField]
    private Button submitMsgBt;

    [SerializeField]
    private Button leaveBt;

    [SerializeField]
    private InputField msgInputfield;

    [SerializeField]
    private Toggle voiceToggle;

    private List<string> MessageArray;

    [Range(1, 5)]
    public int MessageRow = 1;

    public void Start() {
        MessageArray = new List<string>();

        //Room Setting Event
        createRoomBt.onClick.AddListener(() => {

            if (int.TryParse(roomSizeInput.text, out int size)) {
                OnClickCreateRoom(roomNameInput.text, size);
            }
        });

        joinRoomBt.onClick.AddListener(() => {
            OnClickJoinRoom(roomNameInput.text);

        });

        //Chat Setting Event
        submitMsgBt.onClick.AddListener(() => {
            OnMsgSubmit(msgInputfield.text);
            msgInputfield.text = "";
        });

        leaveBt.onClick.AddListener(() => {

        });

        voiceToggle.onValueChanged.AddListener((bool p_enable) => {

            Debug.Log("VoiceToggle " + p_enable);

        });

        DisplayFullMsg();
    }

    #region Room setting 
    private void OnClickCreateRoom(string name, int size)
    {
        pun.CreateRoom(name, size);
    }

    private void OnClickJoinRoom(string name)
    {
        pun.JoinRoom(name);
    }
    #endregion


    #region Chat setting 

    private void OnMsgSubmit(string msg) {
        pun.SendCustomMessage(msg);
    }

    public void OnMsgReceive(string msg)
    {
        if (MessageArray.Count > MessageRow)
            MessageArray.RemoveAt(0);

        MessageArray.Add(msg);
        DisplayFullMsg();
    }

    private void DisplayFullMsg() {
        int msgLength = MessageArray.Count;

        msgDisplayBoard.text = "";
        for (int i = 0; i < msgLength; i++) {
            msgDisplayBoard.text += MessageArray[i] + "\n";
        }
    }

    private void OnVoiceToggle(bool p_enable) {

    }

    private void OnLeaveEvent() { 
        
    }

    #endregion

    public void DisplayCanvas(CanvasGroup canvas, bool isDisplay) {
        canvas.interactable = isDisplay;
        canvas.blocksRaycasts = isDisplay;
        canvas.alpha = (isDisplay) ? 1 : 0;
    }

}
