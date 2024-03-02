using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    [SerializeField] float movingSpeed, camSpeed;
    [SerializeField] GameObject cam, projectile, newProjectile, timeCircle;

    void Start()
    {
        timeCircle.SetActive(false);
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
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * movingSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * movingSpeed * Time.deltaTime;
        }

        //PROJECTILE
        if (Input.GetMouseButtonUp(0))
        {
            newProjectile = Instantiate(projectile, transform.position + (transform.forward * 2), transform.rotation);
            StartCoroutine(TimePause());
        }
    }

    IEnumerator TimePause()
    {
        timeCircle.SetActive(false);
        timeCircle.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.4f);
        Time.timeScale = 1f;
    }

}
