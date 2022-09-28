using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;

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

    public int redCellCount;
    public int greenCellCount;

    [Header("King of the hill")]
    public kingOfTheHill hillKing;
    public float kingOfTheHillWinTime;

    public Slider redZoneSlider;
    public Slider greenZoneSlider;
    // Start is called before the first frame update
    void Start()
    {
        isGrace = true;
        StartCoroutine(gracePeriod());
        manaSlider.minValue = 0;
        manaSlider.maxValue = maxManaAmount;
        cellAmountSlider.minValue = 0;

        redZoneSlider.minValue = 0;
        redZoneSlider.maxValue = kingOfTheHillWinTime;
        greenZoneSlider.minValue = 0;
        greenZoneSlider.maxValue = kingOfTheHillWinTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            GetComponent<PhotonView>().RPC("updateRedZoneSlider", RpcTarget.All, hillKing.redZoneTime);
        }
        else
        {
            GetComponent<PhotonView>().RPC("updateGreenZoneSlider", RpcTarget.All, hillKing.greenZoneTime);
        }

        cellAmountSlider.maxValue = cellAmount;
        cellAmount = redCellCount + greenCellCount;
        cellAmountSlider.value = redCellCount;

        redCellCountText.text = redCellCount.ToString();
        greenCellCountText.text = greenCellCount.ToString();

        manaSlider.value = manaAmount;
        if(manaAmount <= maxManaAmount)
        {
            manaAmount += Time.deltaTime * manaSpeed;
        }
        manaText.text = manaAmount.ToString("0");

        if (currentTime >= thresholdTime)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                GetComponent<PhotonView>().RPC("updateRedCellCount", RpcTarget.All, GameObject.FindGameObjectsWithTag("redCell").Length);
            }
            else
            {
                GetComponent<PhotonView>().RPC("updateGreenCellCount", RpcTarget.All, GameObject.FindGameObjectsWithTag("greenCell").Length);
            }

            //redCellCount = redCells.Length;
            redCells = GameObject.FindGameObjectsWithTag("redCell");
            //greenCellCount = greenCells.Length;

            greenCells = GameObject.FindGameObjectsWithTag("greenCell");
            currentTime = 0;
        }
        else
        {
            currentTime += Time.deltaTime;
        }

        if(!isGrace)
        {
            if(redCellCount >= (cellAmount * 0.9f))
            {
                //win
                Debug.Log("RED CELLS WIN!!!");
                redWin = true;
                GetComponent<PhotonView>().RPC("gameWin", RpcTarget.All, true);

                SceneManager.LoadScene("winScreen");
            }
            if(greenCellCount >= (cellAmount * 0.9f))
            {
                redWin = false;
                Debug.Log("GREEN CELLS WIN!!!");
                GetComponent<PhotonView>().RPC("gameWin", RpcTarget.All, false) ;

                SceneManager.LoadScene("winScreen");
            }
        }


        if(hillKing.redZoneTime >= kingOfTheHillWinTime)
        {
            Debug.Log("RED CELLS WIN!!!");
            redWin = true;
            SceneManager.LoadScene("winScreen");
        }
        if(hillKing.greenZoneTime >= kingOfTheHillWinTime)
        {
            redWin = false;
            Debug.Log("GREEN CELLS WIN!!!");
            SceneManager.LoadScene("winScreen");
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

    [PunRPC]
    void updateRedCellCount(int count)
    {
        redCellCount = count;
    }

    [PunRPC]
    void updateGreenCellCount(int count)
    {
        greenCellCount = count;
    }

    [PunRPC]
    void updateRedZoneSlider(float value)
    {
        redZoneSlider.value = value;
    }

    [PunRPC]
    void updateGreenZoneSlider(float value)
    {
        greenZoneSlider.value = value;
    }

    [PunRPC]
    void gameWin(bool redWinIns)
    {
        redWin = redWinIns;
    }

}
