using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomInputField;
    public TextMeshProUGUI username;
    public GameObject lobbyPanel, roomPanel;
    public TextMeshProUGUI roomName;

    public Transform content;
    public roomItem roomItemPrefab;
    public List<RoomInfo> roomInfoList = new List<RoomInfo>();
    public List<roomItem> roomItemsList = new List<roomItem>();

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby();
        username.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClickCreate()
    {
        if(roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text);
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            foreach (roomItem item in roomItemsList)
            {
                Destroy(item.gameObject);
            }
            roomItemsList.Clear();

            foreach (RoomInfo room in roomList)
            {
                roomItem newRoom = Instantiate(roomItemPrefab, content);
                newRoom.transform.parent = content;
                newRoom.transform.localScale = new Vector3(0.736f, 0.598f, 0.736f);
                newRoom.setRoomName(room.Name);
                roomItemsList.Add(newRoom);
            }

            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        
    }

    public override void OnLeftRoom()
    {
        lobbyPanel.SetActive(true);
        roomPanel.SetActive(false);
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void onClickLeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }
}