using System.Collections;
using UnityEngine;
using Zenject;
using TMPro;

public class BigBetty : MonoBehaviour
{
    [Inject(Id = "ProjectileTextPrefab")] private readonly GameObject projectileTextPrefab;
    [Inject] private readonly IPlayerViewModel playerViewModel;
    [Inject] private DiContainer container;

    public string targetTag = "EnemyHead";
    public float explosionDelay = 3f; // Time before the nuke explodes

    void Start()
    {
        StartCoroutine(WaitAndApplyDamage(explosionDelay));
    }

    private IEnumerator WaitAndApplyDamage(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        // Optionally, you can add an explosion effect here
        ApplyDamageToAllTargets();

        // Destroy the nuke projectile after applying damage
        Destroy(gameObject);
    }

    private void ApplyDamageToAllTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        foreach (GameObject target in targets)
        {
            Transform parentTransform = target.transform.parent;

            if (parentTransform.TryGetComponent(out IDamageable damageable))
            {
                // Apply damage to the target
                int damage = playerViewModel.playerStats.CurrentValue.baseDamage;
                damageable.TakeDamage(damage);

                // Instantiate damage text for visual feedback
                GameObject canvas = GameObject.Find("Canvas");
                GameObject textObject = container.InstantiatePrefab(
                    prefab: projectileTextPrefab,
                    position: target.transform.position,
                    rotation: Quaternion.identity,
                    parentTransform: canvas.transform
                );
                textObject.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
            }
        }
    }
}