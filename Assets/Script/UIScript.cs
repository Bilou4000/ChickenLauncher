using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText1, scoreText2;

    private int scoreResult, scoreToStore;
    private GameObject[] allScore;
    private TMP_Text scoreToGet;

    private void Start()
    {
        scoreResult = 0;
    }

    private void Update()
    {
        //allScore = GameObject.FindGameObjectsWithTag("Score");

        //for (int i = 0; i < allScore.Length; i++)
        //{
        //    scoreToGet = allScore[i].GetComponent<TMP_Text>();
        //    //scoreToStore = int.Parse(scoreToGet.text);

        //    scoreResult += scoreToStore;
        //}

        //scoreText1.text = "Score : " + scoreResult.ToString();
        //scoreText2.text = "Score : " + scoreResult.ToString();
    }
}
