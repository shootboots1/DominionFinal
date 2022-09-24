using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class playerSpawner : MonoBehaviour
{
    public GameObject automon, drudge;
    public Transform automonSpawn, drudgeSpawn;

    public Camera cam;
    public Transform masterClientCamPos, secondaryClientCamPos;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Instantiate(automon.name, automonSpawn.position, Quaternion.identity);
            cam.transform.position = masterClientCamPos.position;
        }
        else
        {
            cam.transform.position = secondaryClientCamPos.position;
            PhotonNetwork.Instantiate(drudge.name, drudgeSpawn.position, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
