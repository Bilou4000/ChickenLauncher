using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] float movingSpeed, camSpeed;
    [SerializeField] GameObject cam, projectile, newProjectile;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        Vector3 camRotation = new Vector3(rotateVertical * camSpeed, 0, 0);
        Vector3 playerRotation = new Vector3(0, -rotateHorizontal * camSpeed, 0);

        transform.eulerAngles -= playerRotation;
        cam.transform.eulerAngles -= camRotation;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * movingSpeed * Time.deltaTime;
        }

        //PROJECTILE
        if(Input.GetMouseButtonUp(0))
        {
            newProjectile = Instantiate(projectile, transform.position + (transform.forward * 2), transform.rotation);
            Destroy(newProjectile, 3);
        }
    }

}