using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cell : MonoBehaviour
{
    public PhotonView view;
    public abilityManager aManager;
    public SpriteRenderer spr;

    [Header("Duplicate")]
    public GameObject cellDuplicate;

    [Header("Dismantle")]
    public GameObject deadCell;
    public GameObject dismantleEffect;

    [Header("Enlarge")]
    public float enlargeAmount;

    [Header("Rush/CellRush")]
    public float rushForce;
    public float cellRushForce;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        aManager = GameObject.FindGameObjectWithTag("abilityManager").GetComponent<abilityManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseEnter()
    {
        if (view.IsMine)
        {
            Debug.Log("enter");
            if (PhotonNetwork.IsMasterClient)
            {
                Debug.Log("masterClient");

                spr.color = (new Color32(111, 30, 30, 255));
            }
            else
            {
                Debug.Log("SecondaryClient");

                spr.color = (new Color32(12, 65, 15, 255));
            }
        }
    }

    void OnMouseExit()
    {
        if (view.IsMine)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                spr.color = (new Color32(156, 44, 44, 255));
            }
            else
            {
                spr.color = (new Color32(22, 99, 27, 255));
            }
        }
    }


    void OnMouseDown()
    {
        if (view.IsMine)
        {
            if (aManager.selectedAbility == SelectedAbility.Duplicate)
            {
                duplicate();
            }
            if (aManager.selectedAbility == SelectedAbility.Dismantle)
            {
                StartCoroutine(dismantle());
            }
            if (aManager.selectedAbility == SelectedAbility.Enlarge)
            {
                enlarge();
            }
            if (aManager.selectedAbility == SelectedAbility.Rush)
            {
                rush();
            }
            if (aManager.selectedAbility == SelectedAbility.CellRush)
            {
                cellRush();
            }
        }    
    }

    void duplicate()
    {
        PhotonNetwork.Instantiate(cellDuplicate.name, transform.position, transform.rotation);
    }

    IEnumerator dismantle()
    {
        GameObject dismantleEff = PhotonNetwork.Instantiate(dismantleEffect.name, transform.position, Quaternion.identity);
        for (int i = 0; i < 5; i++)
        {
            GameObject deadCellInstance = PhotonNetwork.Instantiate(deadCell.name, transform.position + new Vector3(Random.Range(0.01f, 0.1f), Random.Range(0.01f, 1.0f), 0), transform.rotation);
            deadCell.transform.localScale = new Vector3(transform.localScale.x / 2.5f, transform.localScale.y / 2.5f, transform.localScale.z / 2.5f);
            yield return new WaitForSeconds(0.1f);
        }
        PhotonNetwork.Destroy(gameObject);
    }

    void enlarge()
    {
        transform.localScale += new Vector3(enlargeAmount, enlargeAmount, enlargeAmount);
    }

    void rush()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            rb.AddForce(Vector2.left * rushForce);
        }
        else
        {
            rb.AddForce(Vector2.right * rushForce);
        }
    }

    void cellRush()
    {

    }

}
