using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawner : MonoBehaviour
{
    public GameObject redCell;
    public GameObject greenCell;

    public bool isRed;

    public Transform redCellTarget, greenCellTarget;

    private Vector3 dir;

    public float spawnInterval;
    public float spawnForce;
    // Start is called before the first frame update
    void Start()
    {
        redCellTarget = GameObject.FindGameObjectWithTag("automonSpawnTarget").GetComponent<Transform>();
        greenCellTarget = GameObject.FindGameObjectWithTag("drudgeSpawnTarget").GetComponent<Transform>();

        if (isRed)
        {
            StartCoroutine(spawn());
        }
        if (!isRed)
        {
            StartCoroutine(spawnGreen1());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnGreen1()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            GameObject cellInstance = PhotonNetwork.Instantiate(greenCell.name, transform.position, Quaternion.identity);

            dir = greenCellTarget.transform.position - cellInstance.transform.position;
            dir = dir.normalized;
            cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * spawnForce);

            yield return new WaitForSeconds(spawnInterval);

            StartCoroutine(spawnGreen1());
        }
        
    }

    IEnumerator spawn()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            GameObject cellInstance = PhotonNetwork.Instantiate(redCell.name, transform.position, Quaternion.identity);

            dir = redCellTarget.transform.position - cellInstance.transform.position;
            dir = dir.normalized;
            cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * spawnForce);

            yield return new WaitForSeconds(spawnInterval);

            StartCoroutine(spawn());
        }
    }


}
