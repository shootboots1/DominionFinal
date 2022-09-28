using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoSpawner : MonoBehaviour
{
    public GameObject cell;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Instantiate(cell, transform.position, transform.rotation);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Instantiate(cell, transform.position, transform.rotation);
        }
    }
}
