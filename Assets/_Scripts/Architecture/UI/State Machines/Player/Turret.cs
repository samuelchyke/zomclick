using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Zenject;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Turret : MonoBehaviour
{
    [Inject(Id = "ProjectilePrefab")] readonly GameObject projectilePrefab;
    [Inject] public DiContainer container;

    public Transform launchOffest;

    void Start()
    {
        StartCoroutine(FireProjectile());
    }

    IEnumerator FireProjectile()
    {
        while(true)
        {
            yield return new WaitForSeconds(0.3f);

            var projectile = container.InstantiatePrefab(projectilePrefab, launchOffest.transform.position, gameObject.transform.rotation, null);
            projectile.GetComponent<Projectile>();
        }
    }
}
