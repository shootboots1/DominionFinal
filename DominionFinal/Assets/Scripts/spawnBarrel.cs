using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawnBarrel : MonoBehaviour
{
    public GameObject cell;

    public Transform spawnPos;
    public Transform dirTarget;
    public Vector3 dir;

    public float force;
    public float spawnInterval;

    public bool isGame = false;
    // Start is called before the first frame update
    void Start()
    {
        if (!isGame)
        {
            StartCoroutine(spawn());

        }
        else
        {
            StartCoroutine(spawn1());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawn1()
    {
        yield return new WaitForSeconds(spawnInterval);
        GameObject cellInstance = PhotonNetwork.Instantiate(cell.name, spawnPos.position, Quaternion.identity);
        dir = dirTarget.transform.position - cellInstance.transform.position;
        dir = dir.normalized;
        cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * force);
        StartCoroutine(spawn());
    }

    IEnumerator spawn()
    {
        yield return new WaitForSeconds(spawnInterval);
        GameObject cellInstance = Instantiate(cell, spawnPos.position, Quaternion.identity);
        dir = dirTarget.transform.position - cellInstance.transform.position;
        dir = dir.normalized;
        cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * force);
        StartCoroutine(spawn());
    }
}
