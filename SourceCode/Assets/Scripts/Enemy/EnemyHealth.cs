using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 5;

    private int currentHealth;
    private Knockback knockback;
    public DropableItem[] items;
    AudioManager audioManager;

    private void Awake()
    {
        knockback = GetComponent<Knockback>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        currentHealth = health;
    }

    public int GetBaseHealth()
    {
        return health;
    }

    private void OnDestroy()
    {
        EnemySpawner spawner = FindObjectOfType<EnemySpawner>();
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
    }

    public void SetHealth(int newHealth)
    {
        health = newHealth;
        Debug.Log("Enemy health set to: " + health);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        audioManager.PlaySFX(audioManager.hit);
        knockback.GetKnockedBack(PlayerController.Instance.transform, 15f);
        DetectDeath();
    }

    private void DetectDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
            DropItem();
        }
    }

    public void DropItem()
    {
        foreach (var item in items)
        {
            float randomValue = Random.Range(0, 10);

            if (randomValue == item.dropChance)
            {
                Instantiate(item.itemPrefab, transform.position, Quaternion.identity);
                break;
            }
        }
    }
}

[System.Serializable]
public class DropableItem
{
    public GameObject itemPrefab;

    [Range(0, 10)]
    public float dropChance;
}
