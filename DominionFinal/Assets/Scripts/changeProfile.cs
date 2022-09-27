using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

public class changeProfile : MonoBehaviour
{
    public PostProcessVolume camLayer;
    public PostProcessProfile gameProfile;
    public PostProcessProfile kingOfTheHillProfile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("enter");

        if (collision.gameObject.CompareTag("kingOfTheHill"))
        {
            camLayer.profile = kingOfTheHillProfile;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit");

        if (collision.gameObject.CompareTag("kingOfTheHill"))
        {
            camLayer.profile = gameProfile;
        }
    }
}
