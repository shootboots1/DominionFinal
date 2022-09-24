using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject[] redCells;
    public GameObject[] greenCells;

    public float currentTime;
    public float thresholdTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime >= thresholdTime)
        {
            redCells = GameObject.FindGameObjectsWithTag("redCell");
            greenCells = GameObject.FindGameObjectsWithTag("greenCell");
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }
}
