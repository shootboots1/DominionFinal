using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class connectToServer : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameField;
    public Button connectButton;

    public TextMeshProUGUI connecting;

    [Header("Error Messages")]
    public string usernameNull;
    public GameObject errorMessage;
    public Transform canvas;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onClickConnect()
    {
        if(usernameField.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameField.text;
            connecting.gameObject.SetActive(true);
            PhotonNetwork.AutomaticallySyncScene = true;
            PhotonNetwork.ConnectUsingSettings();
        }
        else
        {
            GameObject error = Instantiate(errorMessage, new Vector3(7.86f, 2.6f, 0), Quaternion.identity);
            error.transform.parent = canvas;
            error.transform.localScale = new Vector3(1, 1, 1);
            errorMessage.GetComponent<TextMeshProUGUI>().text = usernameNull;
            Destroy(error, 3f);
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("connected to lobby");
        SceneManager.LoadScene("Lobby");
    }
}
