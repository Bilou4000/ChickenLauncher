using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{

    [SerializeField] private float movingSpeed, camSpeed;
    [SerializeField] private GameObject cam, chicken;

    [Header("Shoot")]
    [SerializeField] private GameObject timeCircle;
    [SerializeField] private GameObject chargingProjectile, shootingProjectile, arm;

    [Header("Sound")]
    [SerializeField] private AudioSource sfxChargingWeapon;
    [SerializeField] private AudioSource sfxTimeshoot;

    private Animator armAnimator;

    void Start()
    {
        armAnimator = arm.GetComponent<Animator>();

        timeCircle.SetActive(false);
        chargingProjectile.SetActive(false);
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
        if (Input.GetMouseButtonDown(0))
        {
            chargingProjectile.SetActive(true);
            sfxChargingWeapon.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            sfxTimeshoot.Play();

            chargingProjectile.SetActive(false);
            sfxChargingWeapon.Stop();

            armAnimator.SetTrigger("Shoot");

            Instantiate(chicken, transform.position + (transform.forward * 2), transform.rotation);
            timeCircle.SetActive(false);
            timeCircle.SetActive(true);


            GameObject shootParticle = Instantiate(shootingProjectile, transform.position + (transform.forward * 2), transform.rotation);
            shootParticle.GetComponent<ParticleSystem>().Play();

            StartCoroutine(TimePause());
        }
    }

    IEnumerator TimePause()
    {
        yield return new WaitForSeconds(0.1f);
        Time.timeScale = 0.3f;
        yield return new WaitForSeconds(0.3f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(0.5f);
    }
}
