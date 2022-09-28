using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleChangeGreen : MonoBehaviour
{
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
        if (collision.gameObject.CompareTag("redCell"))
        {
            Debug.Log("hitRedCell");
            if (transform.localScale.x > collision.transform.localScale.x)
            {
                Debug.Log("scaleSmallGreen");
                transform.localScale = new Vector3(transform.localScale.x - collision.transform.localScale.x, transform.localScale.y - collision.transform.localScale.y, 0);
            }
            else if (transform.localScale.x < collision.transform.localScale.x)
            {
                Debug.Log("scaleSmallRed");
                collision.transform.localScale = new Vector3(collision.transform.localScale.x - transform.localScale.x, collision.transform.localScale.y - transform.localScale.y, 0);
            }
        }
    }
}
