using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiCell : MonoBehaviour
{
    public int function;
    public float currentTime;
    public float timeThreshold;

    public Rigidbody2D rb;
    public TitleSimManager manager;

    public bool isRed;

    [Header("Duplicate")]
    public GameObject cell;

    [Header("Dismantle")]
    public GameObject dismantledCell;

    [Header("Enlarge")]
    public float enlargeAmount;

    [Header("Rush/Cell Rush")]
    public float rushForce;
    public float cellRushForce;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = 0;
        timeThreshold = Random.Range(3.00f, 13.00f);
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("GameController").GetComponent<TitleSimManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime >= timeThreshold)
        {
            if(Random.Range(1, 50) == 1)
            {
                cellRush();
            }
            else if(Random.Range(1, 4) <= 2)
            {
                duplicate();
            }
            else if(Random.Range(1.0f, 2.5f) <= 1.5f)
            {
                dismantle();
            }
            else if(Random.Range(1, 3) == 1)
            {
                enlarge();
            }
            else
            {
                rush();
            }

            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }
    }

    void duplicate()
    {
        Instantiate(cell, transform.position, transform.rotation);
    }

    void dismantle()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject deadCell = Instantiate(dismantledCell, transform.position + new Vector3(Random.Range(0.05f, 0.1f), Random.Range(0.05f, 0.1f), 0), transform.rotation);
            deadCell.transform.localScale = new Vector3(transform.localScale.x / 2.5f, transform.localScale.y / 2.5f, transform.localScale.z / 2.5f);
        }
        Destroy(gameObject);
    }

    void enlarge()
    {
        transform.localScale += new Vector3(enlargeAmount, enlargeAmount, 0);
    }

    void rush()
    {
        if (isRed)
        {
            rb.AddForce(Vector2.right * rushForce);
        }
        else
        {
            rb.AddForce(Vector2.left * rushForce);
        }
    }

    void cellRush()
    {
        if (isRed)
        {
            foreach (GameObject cell in manager.redCells)
            {
                rb.AddForce(Vector2.right * cellRushForce);
            }
        }
        else
        {
            foreach (GameObject cell in manager.greenCells)
            {
                rb.AddForce(Vector2.left * cellRushForce);
            }
        }
    }
}
