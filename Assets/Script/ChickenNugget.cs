using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChickenNugget : MonoBehaviour
{
    [SerializeField] private float spawnForce;

    private void Start()
    {
        //GetComponent<Rigidbody>().AddForce(Vector3.up * spawnForce, ForceMode.Impulse);
        Destroy(gameObject, 20);
    }
}
