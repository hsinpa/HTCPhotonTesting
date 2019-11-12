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

    public void Start() {

        createRoomBt.onClick.AddListener(() => {

            if (int.TryParse(roomSizeInput.text, out int size)) {
            OnClickCreateRoom(roomNameInput.text, size);
            }
        });

        joinRoomBt.onClick.AddListener(() => {
            OnClickJoinRoom(roomNameInput.text);
            
        });
    }

    private void OnClickCreateRoom(string name, int size) {
        pun.CreateRoom(name, size);
    }

    private void OnClickJoinRoom(string name)
    {
        pun.JoinRoom(name);
    }



    public void DisplayCanvas(CanvasGroup canvas, bool isDisplay) {
        canvas.interactable = isDisplay;
        canvas.blocksRaycasts = isDisplay;
        canvas.alpha = (isDisplay) ? 1 : 0;
    }

}
