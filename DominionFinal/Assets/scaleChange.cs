using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class scaleChange : MonoBehaviour
{
    public bool isRed;

    public PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            //transform.localScale = new Vector3(transform.localScale.x * 0.85f, transform.localScale.y * 0.85f, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("greenCell"))
        {
            Debug.Log("hitGreenCell");
            if (transform.localScale.x > collision.transform.localScale.x)
            {
                int id = transform.gameObject.GetComponent<PhotonView>().ViewID;

                view.RPC("scaleChangeInt", RpcTarget.All, id, transform.localScale.x - collision.transform.localScale.x, transform.localScale.y - collision.transform.localScale.y, 1);
                Debug.Log("scaleSmallRed");
            }
            else if (transform.localScale.x < collision.transform.localScale.x)
            {
                Debug.Log("scaleSmallGreen");
                int id = collision.transform.gameObject.GetComponent<PhotonView>().ViewID;
                view.RPC("scaleChangeInt", RpcTarget.All, id, collision.transform.localScale.x - transform.localScale.x, collision.transform.localScale.y - transform.localScale.y, 1);
            }
        }
    }

    [PunRPC]
    void scaleChangeInt(int viewID, float x, float y, float z)
    {
        PhotonView.Find(viewID).gameObject.transform.localScale = new Vector3(x, y, z);
    }
}
