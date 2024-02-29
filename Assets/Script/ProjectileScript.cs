using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.XR;
using static UnityEditor.PlayerSettings;

public class ProjectileScript : MonoBehaviour
{
    //NEED TO FIND A SOLUTION FOR CHICKEN_NUGGET BOUNCING ON CHICKEN + CHICKEN ON TOP OF CHICKEN
    //***********************************************************************************************************

    [SerializeField] private GameObject arm, explosion, chickenNugget, projectileChickenNugget, featherProjectile;
    [SerializeField] private GameObject[] allChickenNuggets;
    [SerializeField] private float projectileSpeed;

    [Header("Score")]
    [SerializeField] private float scoreYOffset;
    private GameObject score1, score2, canva;
    private TMP_Text scoreText1, scoreText2;

    private Vector3 posToGoTo;
    private Animator animator;
    private bool canRun = false;

    private void Awake()
    {
        arm = GameObject.Find("InvisbleArm");
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        canva = GameObject.Find("Canvas");
        score1 = GameObject.Find("Score1");
        score2 = GameObject.Find("Score2");

        GetComponent<Rigidbody>().AddForce(projectileSpeed * (-arm.transform.right), ForceMode.Impulse);
        animator.SetBool("Fly", true);
    }

    private void Update()
    {

        allChickenNuggets = GameObject.FindGameObjectsWithTag("ChickenNugget");

        if (canRun)
        {
            MoveToChicken();
        }
    }

    private void FixedUpdate()
    {
        if(canRun && allChickenNuggets != null && canRun && allChickenNuggets.Length != 0)
        {

            GetComponentInChildren<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            transform.LookAt(posToGoTo);
            animator.SetBool("Fly", true);

            transform.position = Vector3.MoveTowards(transform.position, posToGoTo, 0.1f);
        }    
    }

    private void MoveToChicken()
    {
        float minDistance = 50;

        if (allChickenNuggets != null || allChickenNuggets.Length != 0)
        {
            foreach (GameObject nugget in allChickenNuggets)
            {
                float nuggetDistance = Vector3.Distance(nugget.transform.position, transform.position);
                if (nuggetDistance < minDistance)
                {
                    minDistance = nuggetDistance;
                    posToGoTo = nugget.transform.position;
                }
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            animator.SetBool("Fly", false);
            animator.SetTrigger("Smashed");
            StartCoroutine("Explosion");
        }

        if (collision.gameObject.CompareTag("ChickenNugget"))
        {
            canRun = false;
            animator.SetBool("Fly", false);
            animator.SetBool("Eat", true);
            Destroy(transform.parent.gameObject, 15);
        }

        if (collision.gameObject.CompareTag("Floor") && !animator.GetCurrentAnimatorStateInfo(0).IsName("NewSmash"))
        {
            animator.SetBool("Fly", false);
            canRun = true;
        }
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3);

        //Explosion
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 2);

        //Score
        int randomScore = Random.Range(100,500);
        float randomRotation = Random.Range(-10, 10);
        float newFontSize = (randomScore / 100) * 3.5f;

        GameManager.Instance.ChangeScore(randomScore);

        GameObject theScore1 = Instantiate(score1, Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0, scoreYOffset, 0), Quaternion.Euler(0,0,randomRotation));
        theScore1.transform.SetParent(canva.transform, true);
        theScore1.transform.SetSiblingIndex(0);

        GameObject theScore2 = Instantiate(score2, Camera.main.WorldToScreenPoint(gameObject.transform.position) + new Vector3(0, scoreYOffset - 4, 0), Quaternion.Euler(0, 0, randomRotation));
        theScore2.transform.SetParent(canva.transform, true);

        scoreText1 = theScore1.GetComponent<TMP_Text>();
        scoreText2 = theScore2.GetComponent<TMP_Text>();
        scoreText1.text = randomScore.ToString();
        scoreText2.text = randomScore.ToString();
        scoreText1.fontSize = newFontSize;
        scoreText2.fontSize = newFontSize;

        theScore1.SetActive(true);
        theScore2.SetActive(true);

        Destroy(theScore1, 1);
        Destroy(theScore2, 1);

        for (int i = 0; i < 3; i++)
        {
            Instantiate(projectileChickenNugget, transform.position, Quaternion.identity);
        }

        for (int i = 0; i < 20; i++)
        {
            Instantiate(featherProjectile, transform.position, Random.rotation);
        }

        //ChickenNugget instantiate
        Instantiate(chickenNugget, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(chickenNugget, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 0.2f), transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
