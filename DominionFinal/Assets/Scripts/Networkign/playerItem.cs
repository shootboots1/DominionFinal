using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class playerItem : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public GameObject masterClientAnimation;
    public GameObject secondaryClientAnimation;



    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setPlayerInfo(Player _player)
    {
        playerName.text = _player.NickName;
        if (_player.IsMasterClient)
        {
            masterClientAnimation.SetActive(true);
            secondaryClientAnimation.SetActive(false);
        }
        else
        {
            masterClientAnimation.SetActive(false);
            secondaryClientAnimation.SetActive(true);
        }
    }
}
