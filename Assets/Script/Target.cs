using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] ParticleSystem smokeImpact;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine("ParticlePlay");
    }

    IEnumerator ParticlePlay()
    {
        smokeImpact.Play();
        yield return new WaitForSeconds(0.5f);
        smokeImpact.Stop();
    }
}
