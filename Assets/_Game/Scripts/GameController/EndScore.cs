using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI endScore;
    // Start is called before the first frame update
    void Start()
    {
        int score = PlayerPrefs.GetInt("Score");
        endScore.text = score.ToString();
    }

    
}
