using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Target : MonoBehaviour
{
    [SerializeField] GameObject player, smokeImpact, destroyImpact, destroyImpact2;
    [SerializeField] AudioSource sfxRock;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("ParticlePlay");
    }

    private IEnumerator ParticlePlay()
    {
        Debug.Log(player.transform.rotation);
        GameObject particle = Instantiate(smokeImpact, transform.position, transform.rotation);
        particle.transform.forward = player.transform.forward;

        particle.GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.5f);
        particle.GetComponent<ParticleSystem>().Stop();
    }

    public void Destroy()
    {
        sfxRock.Play();

        GameObject particle = Instantiate(destroyImpact, transform.position, transform.rotation);
        GameObject particle2 = Instantiate(destroyImpact2, transform.position, transform.rotation);

        particle.GetComponentInChildren<ParticleSystem>().Play();
        particle2.GetComponent<ParticleSystem>().Play();

        Destroy(gameObject);
    }
}
