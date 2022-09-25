using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public GameObject[] redCells;
    public GameObject[] greenCells;

    public float currentTime;
    public float thresholdTime;

    public float manaAmount;
    public float maxManaAmount;

    public TextMeshProUGUI manaText;

    public Slider manaSlider;

    public float manaSpeed;

    public int cellAmount;
    public Slider cellAmountSlider;

    public TextMeshProUGUI redCellCountText;
    public TextMeshProUGUI greenCellCountText;

    public int gracePeriodLength;
    public bool isGrace;

    public TextMeshProUGUI gracePeriodText;
    public TextMeshProUGUI gracePeriodNumber;

    public static bool redWin;
    // Start is called before the first frame update
    void Start()
    {
        isGrace = true;
        StartCoroutine(gracePeriod());
        manaSlider.minValue = 0;
        manaSlider.maxValue = maxManaAmount;
        cellAmountSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        cellAmountSlider.maxValue = cellAmount;
        cellAmount = greenCells.Length + redCells.Length;
        cellAmountSlider.value = redCells.Length;

        redCellCountText.text = redCells.Length.ToString();
        greenCellCountText.text = greenCells.Length.ToString();

        manaSlider.value = manaAmount;
        if(manaAmount <= maxManaAmount)
        {
            manaAmount += Time.deltaTime * manaSpeed;
        }
        manaText.text = manaAmount.ToString("0");

        if (currentTime >= thresholdTime)
        {
            redCells = GameObject.FindGameObjectsWithTag("redCell");
            greenCells = GameObject.FindGameObjectsWithTag("greenCell");
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if(!isGrace)
        {
            if(redCells.Length >= (cellAmount * 0.9f))
            {
                //win
                Debug.Log("RED CELLS WIN!!!");
                redWin = true;
                SceneManager.LoadScene("winScreen");
            }
            if(greenCells.Length >= (cellAmount * 0.9f))
            {
                redWin = false;
                Debug.Log("GREEN CELLS WIN!!!");
                SceneManager.LoadScene("winScreen");
            }
        }
    }

    IEnumerator gracePeriod()
    {
        StartCoroutine(gracePeriodCountDown());
        yield return new WaitForSeconds(gracePeriodLength);
        isGrace = false;
    }

    IEnumerator gracePeriodCountDown()
    {
        for (int i = gracePeriodLength; i > 0; i--)
        {
            gracePeriodNumber.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        Destroy(gracePeriodNumber.gameObject);
        gracePeriodText.gameObject.SetActive(true);
        gracePeriodText.text = "Grace period over!";
        yield return new WaitForSeconds(3);
        Destroy(gracePeriodText.gameObject);
    }
}
