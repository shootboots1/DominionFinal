using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kingOfTheHill : MonoBehaviour
{
    public float redCellMass;
    public float greenCellMass;
    public float redZoneTime;
    public float greenZoneTime;

    public List<GameObject> redCells = new List<GameObject>();
    public List<GameObject> greenCells = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(updateZoneTime(true));
        StartCoroutine(updateZoneTime(false));
    }

    // Update is called once per frame
    void Update()
    {
        redCellMass = 0;
        greenCellMass = 0;

        foreach (GameObject cell in redCells)
        {
            redCellMass += cell.transform.localScale.x;
        }
        foreach (GameObject cell in greenCells)
        {
            greenCellMass += cell.transform.localScale.x;
        }
    }

    IEnumerator updateZoneTime(bool isRed)
    {
        yield return new WaitForSeconds(1);
        if (isRed)
        {
            redZoneTime += redCellMass;
            StartCoroutine(updateZoneTime(true));
        }
        else
        {
            greenZoneTime += greenCellMass;
            StartCoroutine(updateZoneTime(false));
        }
    }

    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redCell"))
        {
            collision.GetComponent<Cell>().inZone();
            redCells.Add(collision.gameObject);
            collision.GetComponentInChildren<SpriteRenderer>().color = (new Color32(82, 5, 47, 255));
        }
        if (collision.gameObject.CompareTag("greenCell"))
        {
            collision.GetComponent<Cell>().inZone();
            greenCells.Add(collision.gameObject);
            collision.GetComponentInChildren<SpriteRenderer>().color = (new Color32(70, 96, 73, 255));

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("redCell"))
        {
            collision.GetComponent<Cell>().outZone();
            redCells.Remove(collision.gameObject);
            collision.GetComponentInChildren<SpriteRenderer>().color = (new Color32(156, 44, 44, 255));

        }
        if (collision.gameObject.CompareTag("greenCell"))
        {
            collision.GetComponent<Cell>().outZone();
            greenCells.Remove(collision.gameObject);
            collision.GetComponentInChildren<SpriteRenderer>().color = (new Color32(22, 99, 27, 255));

        }
    }
}
