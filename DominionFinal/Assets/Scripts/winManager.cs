using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class winManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI winText;

    public string redWins;
    public string greenWins;

    public GameObject automon;
    public GameObject drudge;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.redWin)
        {
            winText.text = redWins;
            drudge.SetActive(false);
            automon.SetActive(true);
        }
        if (!gameManager.redWin)
        {
            winText.text = greenWins;
            drudge.SetActive(true);
            automon.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goBackToLobby()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.JoinLobby();

        SceneManager.LoadScene("Lobby");
    }
}
