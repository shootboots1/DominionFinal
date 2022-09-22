using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TitleSimManager : MonoBehaviour
{
    public GameObject[] redCells;
    public GameObject[] greenCells;
    public GameObject[] allCells;

    public float currentTime;
    public float thresholdTime = 3;

    public GameObject deathEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime >= thresholdTime)
        {
            redCells = GameObject.FindGameObjectsWithTag("redCell");
            greenCells = GameObject.FindGameObjectsWithTag("greenCell");
            allCells = FindGameObjectsWithTags(new string[] { "redCell", "greenCell"});
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if(allCells.Length > 950)
        {
            cellReset();
        }

    }

    void cellReset()
    {
        foreach (GameObject cell in allCells)
        {
            GameObject effect = Instantiate(deathEffect, cell.transform.position, Quaternion.identity);
            Destroy(cell);
            Destroy(effect, 2f);
        }
    }

    GameObject[] FindGameObjectsWithTags(params string[] tags)
    {
        var all = new List<GameObject>();

        foreach (string tag in tags)
        {
            var temp = GameObject.FindGameObjectsWithTag(tag).ToList();
            all = all.Concat(temp).ToList();
        }

        return all.ToArray();
    }
}
