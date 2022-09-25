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

    public GameObject redSpawner, greenSpawner;
    public Transform masterBarrelSpawn, secondaryBarrelSpawn;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("masterClientSpawnPoint");

            //PhotonNetwork.Instantiate(automon.name, automonSpawn.position, Quaternion.identity);
            cam.transform.position = masterClientCamPos.position;
            GameObject redSpawnerIns = PhotonNetwork.Instantiate(redSpawner.name, masterBarrelSpawn.position, Quaternion.identity);
            redSpawnerIns.GetComponent<spawner>().isRed = true;
        }
        else if(PhotonNetwork.LocalPlayer == PhotonNetwork.CurrentRoom.Players[2])
        {
            Debug.Log("playerSpawnerPlay");

            //PhotonNetwork.Instantiate(drudge.name, drudgeSpawn.position, Quaternion.identity);
            cam.transform.position = secondaryClientCamPos.position;
            GameObject greenSpawnerIns = PhotonNetwork.Instantiate(greenSpawner.name, secondaryBarrelSpawn.position, Quaternion.identity);
            greenSpawnerIns.GetComponent<spawner>().isRed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
