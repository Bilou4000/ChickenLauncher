using UnityEngine;

public class ProjectileChickenImpactScript : MonoBehaviour
{
    [SerializeField] private float spawnForce;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * spawnForce, ForceMode.Impulse);
        Destroy(gameObject, 3);
    }
}
