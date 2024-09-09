using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;
using Unity.VisualScripting;
using TMPro;

public class BigBetty : MonoBehaviour
{
    [Inject(Id = "ProjectileTextPrefab")] readonly GameObject projectileTextPrefab;
    [Inject] readonly IPlayerViewModel playerViewModel;
    [Inject] DiContainer container;

    public string targetTag = "EnemyHead";

    void Start()
    {
        StartCoroutine(WaitAndApplyDamage(3f)); // Start the coroutine to wait 3 seconds before applying damage
    }

    IEnumerator WaitAndApplyDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime); // Wait for the specified time
        ApplyDamageToAllTargets();
        Destroy(gameObject); // Destroy the nuke projectile after applying damage
    }

    void ApplyDamageToAllTargets()
    {                                                                                           
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject target in targets)
        {
            if (target.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(playerViewModel.playerStats.CurrentValue.baseDamage);

                GameObject canvas = GameObject.Find("Canvas");
                GameObject textObject = container.InstantiatePrefab(projectileTextPrefab, target.transform.position, target.transform.rotation, canvas.transform);
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = playerViewModel.playerStats.CurrentValue.baseDamage.ToString();
            }
        }
    }

    // void OnCollisionEnter2D(Collision2D enemy)
    // {
    //     if (enemy.gameObject.TryGetComponent(out IDamageable damageable))
    //     {
    //         damageable.TakeDamage(playerViewModel.playerStats.CurrentValue.baseDamage);
    //         GameObject canvas = GameObject.Find("Canvas");
    //         GameObject textObject = container.InstantiatePrefab(projectileTextPrefab, enemy.gameObject.transform.position, enemy.gameObject.transform.rotation, canvas.transform);
    //         textObject.GetComponentInChildren<TextMeshProUGUI>().text = playerViewModel.playerStats.CurrentValue.baseDamage.ToString();
    //     }
        // Destroy(gameObject);
    // }
}
