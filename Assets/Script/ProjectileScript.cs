using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] private GameObject arm, explosion, chickenNugget;
    [SerializeField] private GameObject[] allChickenNuggets;
    [SerializeField] private float projectileSpeed;

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
        GetComponent<Rigidbody>().AddForce(projectileSpeed * (-arm.transform.right), ForceMode.Impulse);
        animator.SetBool("Fly", true);
    }

    private void Update()
    {

        allChickenNuggets = GameObject.FindGameObjectsWithTag("ChickenNugget");

        if (canRun)
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
            Destroy(transform.parent.gameObject, 12);
        }

        if (collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("Fly", false);
            canRun = true;
        }

        //if (collision.gameObject.CompareTag("Chicken"))
        //{
        //    Destroy(gameObject);
        //}
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(3);
        explosion = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosion, 2);

        Instantiate(chickenNugget, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        Instantiate(chickenNugget, new Vector3(transform.position.x + 0.3f, transform.position.y, transform.position.z + 0.2f), transform.rotation);
        Destroy(transform.parent.gameObject);
    }
}
