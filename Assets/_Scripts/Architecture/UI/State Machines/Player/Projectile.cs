using UnityEngine;
using Zenject;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;
    public string targetTag = "EnemyHead";

    [Inject] readonly IPlayerViewModel playerViewModel;

    private Vector3 direction;
    private bool hasTarget = false;
    public float spinSpeed = 720f; // Degrees per second for spinning

    public float bulletDropRate = 9.81f; 
    private float timeSinceFired = 0f; 
    private bool isSpinning = false; // Flag to check if projectile is spinning



    void Start()
    {
        if (!FindRandomTargetAndSetDirection())
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (hasTarget)
        {
            transform.position += projectileSpeed * Time.deltaTime * direction;
        }
        else
        {
            timeSinceFired += Time.deltaTime;
            // Calculate the bullet drop
            float drop = 0.5f * bulletDropRate * timeSinceFired * timeSinceFired;

            Vector3 dropVector = new Vector3(0, -drop, 0);
            transform.position += (projectileSpeed * Time.deltaTime * direction) + dropVector;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction - new Vector3(0, drop, 0));
        }

        if (isSpinning)
        {
            transform.Rotate(Vector3.forward, spinSpeed * Time.deltaTime);
        }
    }

    bool FindRandomTargetAndSetDirection()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        if (targets.Length > 0)
        {
            GameObject randomTarget = targets[UnityEngine.Random.Range(0, targets.Length)];
            direction = (randomTarget.transform.position - transform.position).normalized;
            hasTarget = true;
            return true; // Target was found
        }
        return false; // No target found
    }

    bool FindNearestTargetAndSetDirection()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestTarget = null;

        foreach (GameObject potentialTarget in targets)
        {
            float distance = Vector3.Distance(transform.position, potentialTarget.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestTarget = potentialTarget;
            }
        }

        if (nearestTarget != null)
        {
            direction = (nearestTarget.transform.position - transform.position).normalized;
            hasTarget = true;
            return true; // Target was found
        }
        return false; // No target found
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(playerViewModel.playerStats.totalDamage);
            Destroy(gameObject);
        }
        // else if (other.gameObject.CompareTag("Floor"))
        // {
        //     isSpinning = true;
        //     projectileSpeed *= 0.5f;
        // }
    }
}