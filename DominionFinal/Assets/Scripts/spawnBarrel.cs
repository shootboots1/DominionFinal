using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBarrel : MonoBehaviour
{
    public GameObject cell;

    public Transform spawnPos;

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
        Instantiate(cell, spawnPos.position, Quaternion.identity);
        StartCoroutine(spawn());
    }
}
