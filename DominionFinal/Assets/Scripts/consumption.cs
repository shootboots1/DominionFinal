using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class consumption : MonoBehaviour
{
    public bool isRed;

    public GameObject Redeffect;
    public GameObject Greeneffect;

    public kingOfTheHill king;

    public PhotonView view;

    public float thresholdTime;
    public float currentTime;

    public Cell cellScr;

    public float scaleChangeScaler = 0.9f;
    // Start is called before the first frame update
    void Start()
    {
        
        currentTime = 0;
        view = GetComponent<PhotonView>();
        king = GameObject.FindGameObjectWithTag("kingOfTheHill").GetComponent<kingOfTheHill>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRed)
        {
            if (collision.gameObject.CompareTag("greenCell"))
            {
                if (cellScr.mass > collision.transform.GetComponent<Cell>().mass)
                {
                    if (king.greenCells.Contains(collision.gameObject))
                    {
                        king.greenCells.Remove(collision.gameObject);
                    }
                    GameObject eff = PhotonNetwork.Instantiate(Greeneffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff, 2);

                    int scaleID = gameObject.GetComponent<PhotonView>().ViewID;
                    view.RPC("scaleChange", RpcTarget.All, collision.transform.GetComponent<Cell>().mass, scaleID);

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;
                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);

                }
                
                else if (cellScr.mass == collision.transform.GetComponent<Cell>().mass)
                {

                    if (king.redCells.Contains(gameObject))
                    {
                        king.redCells.Remove(gameObject);
                    }

                    GameObject eff = PhotonNetwork.Instantiate(Redeffect.name, transform.position, Quaternion.identity);
                    Destroy(eff, 2);
                    GameObject eff1 = PhotonNetwork.Instantiate(Greeneffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff1, 2);

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;
                    cellScr.mass = 0;
                    collision.transform.GetComponent<Cell>().mass = 0;
                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);
                    PhotonNetwork.Destroy(gameObject);
                }

            }
        }
        else
        {
            if (collision.gameObject.CompareTag("redCell"))
            {
                if (cellScr.mass > collision.transform.GetComponent<Cell>().mass)
                {
                    if (king.redCells.Contains(collision.gameObject))
                    {
                        king.redCells.Remove(collision.gameObject);
                    }
                    GameObject eff = PhotonNetwork.Instantiate(Redeffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff, 2);

                    int scaleID = gameObject.GetComponent<PhotonView>().ViewID;
                    view.RPC("scaleChange", RpcTarget.All, collision.transform.GetComponent<Cell>().mass, scaleID);

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;
                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);

                }
            }
        }

    }
    [PunRPC]
    void scaleChange(float scale, int view)
    {
        PhotonView.Find(view).gameObject.GetComponent<Cell>().mass -= (scale * scaleChangeScaler);
    }


    [PunRPC]
    void destroy(int viewID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
