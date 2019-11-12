using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class PunSampleManager : Photon.MonoBehaviour
{
    [SerializeField]
    private PunSampleUI punUI;

    [SerializeField]
    private int Version = 1;

    // Start is called before the first frame update
    public void Start()
    {
        punUI.DisplayCanvas(punUI.RoomSetting, false);

        PhotonNetwork.ConnectUsingSettings(Version + "." + SceneManagerHelper.ActiveSceneBuildIndex);
    }

    #region PUN 
    public void ConnectToPUN(bool connect)
    {
        if (!PhotonNetwork.connected && connect)
            PhotonNetwork.ConnectUsingSettings(string.Format("1.{0}", UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex));
        else if (!connect)
        {
            PhotonNetwork.Disconnect();
        }
    }
    #endregion
    #region Room 
    public void JoinRoom(string room)
    {
        PhotonNetwork.JoinRoom(room);
    }

    public void CreateRoom(string room, int size)
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = System.Convert.ToByte(size);
        Debug.Log("CreateRoom");
        PhotonNetwork.JoinOrCreateRoom(room, options, TypedLobby.Default);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SendCustomMessage(string message) {
        if (PhotonNetwork.connected) {
            PhotonNetwork.networkingPeer.OpRaiseEvent(OperationCode.CustomEvent, message, true, RaiseEventOptions.Default);
        }
    }
    #endregion

    #region Voice

    #endregion

    #region PUN Callback Event
    public void OnRaiseEvent(string message) {
        Debug.Log("OnRaiseEvent : " + message);
    }

    public virtual void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room. Calling: PhotonNetwork.JoinRandomRoom();");

        punUI.DisplayCanvas(punUI.RoomSetting, true);
    }

    // the following methods are implemented to give you some context. re-implement them as needed.

    public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
    {
        Debug.LogError("Cause: " + cause);
    }

    public void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom() called by PUN. Now this client is in a room. From here on, your game would be running. For reference, all callbacks are listed in enum: PhotonNetworkingMessage");

        SendCustomMessage("");
    }
    #endregion
}
