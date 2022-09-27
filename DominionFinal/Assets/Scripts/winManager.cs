using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class winManager : MonoBehaviour
{
    public TextMeshProUGUI winText;

    public string redWins;
    public string greenWins;

    // Start is called before the first frame update
    void Start()
    {
        if (gameManager.redWin)
        {
            winText.text = redWins;
        }
        if (!gameManager.redWin)
        {
            winText.text = greenWins;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
