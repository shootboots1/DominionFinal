using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameManager : MonoBehaviour
{
    public GameObject[] redCells;
    public GameObject[] greenCells;

    public float currentTime;
    public float thresholdTime;

    public float manaAmount;
    public float maxManaAmount;

    public TextMeshProUGUI manaText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(manaAmount <= maxManaAmount)
        {
            manaAmount += Time.deltaTime * 1.5f;
        }
        manaText.text = manaAmount.ToString("0");

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
