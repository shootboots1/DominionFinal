using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Cell : MonoBehaviour
{
    public PhotonView view;
    public abilityManager aManager;
    public gameManager gManager;
    public SpriteRenderer spr;

    public bool isMaster;

    [Header("Costs")]
    public int duplicateCost;
    public int dismantleCost;
    public int enlargeCost;
    public int rushCost = 0;
    public int cellRushCost;

    [Header("Duplicate")]
    public GameObject cellDuplicate;

    [Header("Dismantle")]
    public GameObject deadCell;
    public GameObject dismantleEffect;
    public bool isDismantle = false;

    [Header("Enlarge")]
    public float enlargeAmount;
    public float mass;

    [Header("Rush/CellRush")]
    public float rushForce;
    public float cellRushForce;

    public Rigidbody2D rb;

    [Header("King of the Hill")]
    public GameObject greenZoneEff;
    public GameObject redZoneEff;
    // Start is called before the first frame update
    void Start()
    {
        if (!isDismantle)
        {
            mass = 1;
        }
        aManager = GameObject.FindGameObjectWithTag("abilityManager").GetComponent<abilityManager>();
        gManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<gameManager>();
        view.OwnershipTransfer = OwnershipOption.Takeover;
        if (isMaster)
        {
            view.TransferOwnership(PhotonNetwork.CurrentRoom.Players[1]);
        }
        else if (!isMaster)
        {
            view.TransferOwnership(PhotonNetwork.CurrentRoom.Players[2]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(mass, mass, 0);
    }

    public void inZone()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            redZoneEff.SetActive(true);
        }
        else
        {
            greenZoneEff.SetActive(true);
        }
    }

    public void outZone()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            redZoneEff.SetActive(false);
        }
        else
        {
            greenZoneEff.SetActive(false);
        }
    }

    void OnMouseEnter()
    {
        if (view.IsMine)
        {
            if (!isDismantle)
            {

                if (PhotonNetwork.IsMasterClient)
                {

                    spr.color = (new Color32(111, 30, 30, 255));
                }
                else
                {
                    Debug.Log("SecondaryClient");

                    spr.color = (new Color32(12, 65, 15, 255));
                }
            }
            
        }
    }

    void OnMouseExit()
    {
        if (!isDismantle)
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
    }


    void OnMouseDown()
    {
        if (view.IsMine)
        {
            if (!isDismantle)
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
    }

    void duplicate()
    {
        if(gManager.manaAmount >= duplicateCost)
        {
            Debug.Log("duplicate");


            PhotonNetwork.Instantiate(cellDuplicate.name, transform.position, transform.rotation);
            gManager.manaAmount -= duplicateCost;
        }
    }

    IEnumerator dismantle()
    {
        if(gManager.manaAmount >= dismantleCost)
        {
            GameObject dismantleEff = PhotonNetwork.Instantiate(dismantleEffect.name, transform.position, Quaternion.identity);
            dismantleEff.transform.rotation = Quaternion.Euler(0, -180, 0);
            Destroy(dismantleEff, 5f);
            for (int i = 0; i < 5; i++)
            {
                GameObject deadCellInstance = PhotonNetwork.Instantiate(deadCell.name, transform.position, transform.rotation);
                deadCellInstance.transform.localScale = new Vector3(transform.localScale.x / 2.5f, transform.localScale.y / 2.5f, transform.localScale.z / 2.5f);
                deadCellInstance.GetComponent<Cell>().mass = deadCellInstance.transform.localScale.x;

                yield return new WaitForSeconds(0.1f);
            }
            gManager.manaAmount -= dismantleCost;
            PhotonNetwork.Destroy(gameObject);
        }
        
    }

    void enlarge()
    {
        if(gManager.manaAmount >= enlargeCost)
        {
            gManager.manaAmount -= enlargeCost;
            view.RPC("cellScale", RpcTarget.All, enlargeAmount);
        }
    }

    void rush()
    {
        if (PhotonNetwork.IsMasterClient)
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
        if (gManager.manaAmount >= cellRushCost)
        {
            gManager.manaAmount -= cellRushCost;
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (GameObject cell in gManager.redCells)
                {
                    cell.GetComponent<Rigidbody2D>().AddForce(Vector2.right * rushForce);
                }
            }
            else
            {
                foreach (GameObject cell in gManager.greenCells)
                {
                    cell.GetComponent<Rigidbody2D>().AddForce(Vector2.left * rushForce);
                }
            }
        }
    }

    [PunRPC]
    void cellScale(float amount)
    {
        mass += amount;
    }
}
