using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private TMP_Text topScoreText1, topScoreText2;
    [SerializeField] private Animator topScoreAnimator;
    private float theScore = 0;

    private void Awake()
    {
        Instance = this;
    }

    public void ChangeScore(int score)
    {
        topScoreAnimator.SetTrigger("Big");

        theScore += score;
        topScoreText1.text = theScore.ToString();
        topScoreText2.text = theScore.ToString();
    }
}
