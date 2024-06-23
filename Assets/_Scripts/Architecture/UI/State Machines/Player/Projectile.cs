using UnityEngine;
using Zenject;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Projectile : MonoBehaviour
{
    [Inject(Id = "ProjectileTextPrefab")] readonly GameObject projectileTextPrefab;

    public float projectileSpeed = 10f;
    public string targetTag = "EnemyHead";

    [Inject] readonly IPlayerViewModel playerViewModel;
    [Inject] DiContainer container;

    public float upwardForce = 0.1f;
    public float fadeOutTime = 2f;

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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(playerViewModel.playerStats.totalDamage);
            Destroy(gameObject);

            Debug.Log("Before instantiating text");
            Canvas uiCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            // Spawn text at collision location

            GameObject textObject = Instantiate(projectileTextPrefab, uiCanvas.transform);
            
            // Vector2 worldPosition = other.GetContact(0).point;
            // Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPosition);
            // RectTransformUtility.ScreenPointToLocalPointInRectangle(uiCanvas.transform as RectTransform, screenPoint, uiCanvas.worldCamera, out Vector2 canvasPos);


            Debug.Log(other.GetContact(0).point);
            textObject.GetComponent<RectTransform>().position = other.GetContact(0).point;

            Debug.Log("After instantiating text");

            

            // Set the text to display the damage dealt
            textObject.GetComponent<TextMeshProUGUI>().text = playerViewModel.playerStats.totalDamage.ToString();

            // Apply upward force to the text object
            // Rigidbody2D rb = textObject.GetComponent<Rigidbody2D>();
            // if (rb != null)
            // {
            //     rb.AddForce(Vector2.up * upwardForce, ForceMode2D.Impulse);
            // }

            // Fade out the text object over time
            StartCoroutine(FadeOutText(textObject, textObject.GetComponent<TextMeshProUGUI>(), fadeOutTime));
        }
        // else if (other.gameObject.CompareTag("Floor"))
        // {
        //     isSpinning = true;
        //     projectileSpeed *= 0.5f;
        // }
    }

    IEnumerator FadeOutText(GameObject m, TextMeshProUGUI text, float fadeTime)
    {
        float elapsedTime = 0f;
        Color originalColor = text.color;

        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeTime);
            text.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(m);
    }
}