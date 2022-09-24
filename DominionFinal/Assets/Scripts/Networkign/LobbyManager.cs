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
    public List<roomItem> roomItemsList = new List<roomItem>();

    public float timeBetweenUpdates = 1.5f;
    float nextUpdateTime;

    public List<playerItem> playerItemsList = new List<playerItem>();
    public playerItem playerItemPrefab;
    public Transform playerItemParent;

    public GameObject playButton;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.JoinLobby();
        username.text = PhotonNetwork.NickName;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 2)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void onClickCreate()
    {
        if(roomInputField.text.Length >= 1)
        {
            PhotonNetwork.CreateRoom(roomInputField.text, new RoomOptions() { MaxPlayers=2});
        }
    }

    public override void OnJoinedRoom()
    {
        lobbyPanel.SetActive(false);
        roomPanel.SetActive(true);
        roomName.text = "Room Name: " + PhotonNetwork.CurrentRoom.Name;

        updatePlayerList();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        if(Time.time >= nextUpdateTime)
        {
            UpdateRoomList(roomList);
            nextUpdateTime = Time.time + timeBetweenUpdates;
        }
        
    }

    public void UpdateRoomList(List<RoomInfo> list)
    {
        foreach (roomItem item in roomItemsList)
        {
            Destroy(item.gameObject);
        }
        roomItemsList.Clear();

        foreach (RoomInfo room in list)
        {
            roomItem newRoom = Instantiate(roomItemPrefab, content);
            newRoom.transform.parent = content;
            newRoom.transform.localScale = new Vector3(0.736f, 0.598f, 0.736f);
            newRoom.setRoomName(room.Name);
            roomItemsList.Add(newRoom);
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

    void updatePlayerList()
    {
        foreach (playerItem item in playerItemsList)
        {
            Destroy(item.gameObject);
        }
        playerItemsList.Clear();

        if (PhotonNetwork.CurrentRoom == null)
        {
            return;
        }

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            playerItem newPlayerItem = Instantiate(playerItemPrefab, playerItemParent);
            newPlayerItem.setPlayerInfo(player.Value);
            playerItemsList.Add(newPlayerItem);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        updatePlayerList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        updatePlayerList();
    }

    public void onClickPlay()
    {
        PhotonNetwork.LoadLevel("Game");
    }
}
