using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class roomItem : MonoBehaviour
{
    public TextMeshProUGUI roomName;
    LobbyManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<LobbyManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setRoomName(string _roomName)
    {
        roomName.text = _roomName;
    }

    public void onClickItem()
    {
        manager.JoinRoom(roomName.text);
    }
}
