using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBarrel : MonoBehaviour
{
    public GameObject cell;

    public Transform spawnPos;
    public Transform dirTarget;
    public Vector3 dir;

    public float force;
    public float spawnInterval;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawn());
    }

    // Update is called once per frame
    void Update()
    {
        
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
