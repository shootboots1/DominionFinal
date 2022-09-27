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
        currentTime += Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRed)
        {
            if (collision.gameObject.CompareTag("greenCell"))
            {
                if(transform.localScale.x > collision.transform.localScale.x)
                {

                    if(king.greenCells.Contains(collision.gameObject))
                    {
                        king.greenCells.Remove(collision.gameObject);
                    }

                    transform.localScale -= new Vector3(collision.transform.localScale.x * 0.85f, collision.transform.localScale.y * 0.85f, 0);

                    GameObject eff = PhotonNetwork.Instantiate(Greeneffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff, 2);

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;

                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);
                }
                else if (transform.localScale.x == collision.transform.localScale.x)
                {
                    
                    if (king.redCells.Contains(gameObject))
                    {
                        king.redCells.Remove(gameObject);
                    }

                    GameObject eff = PhotonNetwork.Instantiate(Redeffect.name, transform.position, Quaternion.identity);
                    Destroy(eff, 2);
                    GameObject eff1 = PhotonNetwork.Instantiate(Greeneffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff1, 2);

                    Debug.Log("RPC");

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;

                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);

                    PhotonNetwork.Destroy(gameObject);
                }

            }

        }
        else
        {
            if (collision.gameObject.CompareTag("redCell"))
            {
                if (transform.localScale.x > collision.transform.localScale.x)
                {

                    if (king.redCells.Contains(collision.gameObject))
                    {
                        king.redCells.Remove(collision.gameObject);
                    }

                    transform.localScale -= new Vector3(collision.transform.localScale.x * 0.85f, collision.transform.localScale.y * 0.85f, 0);

                    GameObject eff = PhotonNetwork.Instantiate(Redeffect.name, collision.transform.position, Quaternion.identity);
                    Destroy(eff, 2);

                    int id = collision.gameObject.GetComponent<PhotonView>().ViewID;

                    view.RPC("destroy", collision.gameObject.GetComponent<PhotonView>().Owner, id);

                }
            }

        }
    }


    [PunRPC]
    void destroy(int viewID)
    {
        PhotonNetwork.Destroy(PhotonView.Find(viewID).gameObject);
    }
}
