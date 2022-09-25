using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class spawner : MonoBehaviour
{
    public GameObject redCell;
    public GameObject greenCell;

    public bool isRed;

    public GameObject redCellPos, greeCellPos;
    public Transform redCellTarget, greenCellTarget;

    private Vector3 dir;

    public float spawnInterval;
    public float spawnForce;
    // Start is called before the first frame update
    void Start()
    {
        redCellPos = GameObject.FindGameObjectWithTag("automonSpawnPos");
        greeCellPos = GameObject.FindGameObjectWithTag("drudgeSpawnPos");
        redCellTarget = GameObject.FindGameObjectWithTag("automonSpawnTarget").GetComponent<Transform>();
        greenCellTarget = GameObject.FindGameObjectWithTag("drudgeSpawnTarget").GetComponent<Transform>();

        if (isRed)
        {
            StartCoroutine(spawn());

        }
        if (!isRed)
        {
            StartCoroutine(spawnGreen());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator spawnGreen()
    {
        if (GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("spawnGreen");

            GameObject cellInstance = PhotonNetwork.Instantiate(greenCell.name, greeCellPos.transform.position, Quaternion.identity);

            dir = greenCellTarget.transform.position - cellInstance.transform.position;
            dir = dir.normalized;
            cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * spawnForce);
            yield return new WaitForSeconds(spawnInterval);

            StartCoroutine(spawnGreen());
        }
        
    }

    IEnumerator spawn()
    {
        
        GameObject cellInstance = PhotonNetwork.Instantiate(redCell.name, redCellPos.transform.position, Quaternion.identity);

        dir = redCellTarget.transform.position - cellInstance.transform.position;
        dir = dir.normalized;
        cellInstance.GetComponent<Rigidbody2D>().AddForce(dir * spawnForce);
        yield return new WaitForSeconds(spawnInterval);

        StartCoroutine(spawn());

    }
}
