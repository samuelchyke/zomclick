using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using R3;

public class Projectile : MonoBehaviour
{
    [Inject(Id = "ProjectileTextPrefab")] readonly GameObject projectileTextPrefab;

    public float projectileSpeed = 10f;
    public string targetTag = "EnemyHead";

    [Inject] EventsManager eventsManager;
    [Inject] readonly IPlayerViewModel playerViewModel;
    [Inject] readonly IPlayerSkillsViewModel playerSkillsViewModel;
    [Inject] DiContainer container;

    public float upwardForce = 0.1f;
    public float fadeOutTime = 2f;

    private Vector3 direction;
    private bool hasTarget = false;
    public float spinSpeed = 720f; // Degrees per second for spinning

    public float bulletDropRate = 9.81f; 
    private float timeSinceFired = 0f; 
    private bool isSpinning = false; // Flag to check if projectile is spinning
    private int damage;

    void Start()
    {
        CalculateDamage();
        if (!FindRandomTargetAndSetDirection())
        {
            Destroy(gameObject);
        }
    }

    void CalculateDamage()
    {
        damage = playerViewModel.playerStats.CurrentValue.baseDamage;
        Debug.Log("projectile intial damage: " + damage);
    
        var lightningRounds = playerSkillsViewModel.lightningRounds.CurrentValue;
        // Debug.Log("projectile lightingRounds activeState: " + lightingRounds.isActive);
        var incendiaryRounds = playerSkillsViewModel.incendiaryRounds.CurrentValue;
        // Debug.Log("projectile incendiaryRounds damage: " + lightingRounds.isActive);

        if(lightningRounds.isActive)
        {
            Debug.Log("projectile before adding lightingRounds dmg: " + damage);
            damage = damage + 1;
            Debug.Log("projectile after adding lightingRounds dmg: " + damage);
        }
        if(incendiaryRounds.isActive)
        {
            Debug.Log("projectile before adding incendiaryRounds dmg: " + damage);
            damage = damage + 1;
            Debug.Log("projectile after adding incendiaryRounds dmg: " + damage);
        }
            
        // playerShopViewModel.playerSkills.Subscribe(skills => 
        // {
        //     if(skills.lightningRounds.isActive)
        //     {
        //         Debug.Log("projectile before adding lightingRounds dmg: " + damage);
        //         damage = damage + 1;
        //         Debug.Log("projectile after adding lightingRounds dmg: " + damage);
        //     }
        //     if(skills.incendiaryRounds.isActive)
        //     {
        //         Debug.Log("projectile before adding incendiaryRounds dmg: " + damage);
        //         damage = damage + 1;
        //         Debug.Log("projectile after adding incendiaryRounds dmg: " + damage);
        //     }
        // });
        Debug.Log("projectile calculated damage: " + damage);
        // Debug.Log("projectile damage: " + damage);
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
            GameObject randomTarget = targets[Random.Range(0, targets.Length)];
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

    void OnCollisionEnter2D(Collision2D enemy)
    {
        var midasRounds = playerSkillsViewModel.midasRounds.CurrentValue;

        if (enemy.gameObject.TryGetComponent(out IDamageable damageable))
        {
            if(midasRounds.isActive)
            {
                playerSkillsViewModel.IncreasePlayerGold();
            }
            damageable.TakeDamage(damage);
            GameObject canvas = GameObject.Find("Canvas");
            GameObject textObject = container.InstantiatePrefab(projectileTextPrefab, enemy.gameObject.transform.position, enemy.gameObject.transform.rotation, canvas.transform);
            textObject.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        }
        Destroy(gameObject);
    }
}