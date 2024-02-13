using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField] GameObject arm;
    [SerializeField] float projectileSpeed;

    private Animator animator;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            animator.SetBool("Fly", false);
            animator.SetTrigger("Smashed");
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

}
