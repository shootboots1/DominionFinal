using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class consumption : MonoBehaviour
{
    public bool isRed;

    public GameObject Redeffect;
    public GameObject Greeneffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isRed)
        {
            if (collision.gameObject.CompareTag("greenCell"))
            {
                if(transform.localScale.x > collision.transform.localScale.x)
                {
                    transform.localScale -= new Vector3(collision.transform.localScale.x, collision.transform.localScale.y, 0);
                    GameObject effect = Instantiate(Greeneffect, collision.transform.position, Quaternion.identity);
                    Destroy(effect, 2);
                    Destroy(collision.gameObject);
                }
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("redCell"))
            {
                if (transform.localScale.x > collision.transform.localScale.x)
                {
                    transform.localScale -= new Vector3(collision.transform.localScale.x, collision.transform.localScale.y, 0);
                    GameObject effect = Instantiate(Redeffect, collision.transform.position, Quaternion.identity);
                    Destroy(effect, 2);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
