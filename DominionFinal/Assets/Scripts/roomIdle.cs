using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roomIdle : MonoBehaviour
{
    public Transform iris;
    public Vector2[] positions;
    public Vector2 startPos;

    public Animator anim;

    private float currentTimeBlink;
    private float currentTimeEyeMove;
    public float thresholdBlink;
    public float thresholdEyeMove;
    // Start is called before the first frame update
    void Start()
    {
        startPos = iris.transform.position;
        anim = GetComponent<Animator>();
        thresholdBlink = Random.Range(3.00f, 10.00f);
        thresholdEyeMove = Random.Range(3.00f, 10.00f);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTimeBlink >= thresholdBlink)
        {
            anim.SetTrigger("blink");
            currentTimeBlink = 0;
        }
        else
        {
            currentTimeBlink += Time.deltaTime;
        }

        if(currentTimeEyeMove >= thresholdEyeMove)
        {
            StartCoroutine(eyeMove());
            currentTimeEyeMove = 0;
        }
        else
        {
            currentTimeEyeMove += Time.deltaTime;
        }
    }

    IEnumerator eyeMove()
    {
        iris.GetComponent<RectTransform>().localPosition = positions[Random.Range(1, positions.Length)];
        yield return new WaitForSeconds(Random.Range(0.5f, 5.0f));
        iris.position = startPos;
    }
}
