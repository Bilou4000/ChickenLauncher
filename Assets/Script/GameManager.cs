using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TMP_Text topScoreText1, topScoreText2;
    private float theScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScore(int score)
    {
        theScore += score;
        topScoreText1.text = "Score : " + theScore.ToString();
        topScoreText2.text = "Score : " + theScore.ToString();
    }
}
