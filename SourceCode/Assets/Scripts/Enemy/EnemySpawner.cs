using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform player; 
    [SerializeField] private float spawnRadius = 10f; 
    [SerializeField] private float spawnOuterRadius = 15f; 
    [SerializeField] private float spawnInterval = 15f;
    [SerializeField] private int maxEnemies = 20;

    private int currentEnemyCount = 0;

    public float difficultyIncreaseInterval = 5f;
    private float elapsedTime = 0f;
    private int damageAndHealthAmp;
    

    private void Start()
    {
        damageAndHealthAmp = 0;
        StartCoroutine(SpawnEnemies());
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;
        if (elapsedTime >= difficultyIncreaseInterval)
        {
            elapsedTime = 0f;
            IncreaseDifficulty();
        }
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            if (currentEnemyCount < maxEnemies)
            {
                Vector2 spawnPosition = GenerateSpawnPosition();
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

                currentEnemyCount++;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private Vector2 GenerateSpawnPosition()
    {
        Vector2 playerPosition = player.position;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

        float distance = Random.Range(spawnRadius, spawnOuterRadius);

        return playerPosition + direction * distance;
    }

    public void EnemyDestroyed()
    {
        currentEnemyCount--;
        Debug.Log(currentEnemyCount);
    }

    private void IncreaseDifficulty()
    {
        damageAndHealthAmp++;
        maxEnemies += 5;
        int newHealth = 0;
        int newDamage = 0;
        int newLevel = 0;

        EnemyHealth enemyHealthScript = enemyPrefab.GetComponent<EnemyHealth>();
        EnemyAI enemyAIScript = enemyPrefab.GetComponent<EnemyAI>();

        if (enemyHealthScript != null)
        {
            newHealth = enemyHealthScript.GetBaseHealth() + damageAndHealthAmp;
        }

        if (enemyAIScript != null)
        {
            newDamage = enemyAIScript.GetBaseDamage() + damageAndHealthAmp;
            newLevel = 1 + damageAndHealthAmp;
        }

        enemyHealthScript.SetHealth(newHealth);
        enemyAIScript.SetDamage(newDamage);
        enemyAIScript.SetLevel(newLevel);
        Debug.Log("difficulty increased");
    }
}
