using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileChickenImpactScript : MonoBehaviour
{
    [SerializeField] private float spawnForce;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * spawnForce, ForceMode.Impulse);
    }
}
