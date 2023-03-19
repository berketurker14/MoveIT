using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2Int currentGridPos;
    public Vector2Int targetGridPos;
    public Vector2Int[] moveDirections;
    public float moveTime = 200f;
    private float timer;
    public float health, damage, experience;
    public string enemyName;
    public GameObject enemyPrefab;
    [HideInInspector] public ObjectPool objectPool;
    [HideInInspector] public EnemySpawner enemySpawner;
    public EnemyTypeHolder enemyTypeHolder;
    public static Enemy instance;

    private void Awake()
    {
        instance = this;
        enemyTypeHolder = EnemyTypeHolder.instance;
    }

    private void Start()
    {
        enemySpawner = EnemySpawner.instance;
        objectPool = ObjectPool.instance;
        moveDirections = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
    }

    private void Update()
    {
        MoveToTarget();
        UpdateHealth();
    }

    private void MoveToTarget()
    {
        if (currentGridPos == targetGridPos)
        {
            ChooseRandomDirection();
            timer = 0f;
        }

        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / moveTime);

        Vector3 targetPos = new Vector3(GridController.Instance.currentGridPosition.x, GridController.Instance.currentGridPosition.y, 0f);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;

        Vector2Int newGridPos = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        if (newGridPos != currentGridPos)
        {
            currentGridPos = newGridPos;
            timer = 0f;
        }

        transform.position = Vector3.Lerp(transform.position, targetPos, t);
    }

    private void ChooseRandomDirection()
    {
        Vector2Int randomDir = moveDirections[Random.Range(0, moveDirections.Length)];
        targetGridPos = currentGridPos + randomDir;
    }

    private void UpdateHealth()
    {
        health -= Time.deltaTime * 15;
        if (health < 0f)
        {
            health = 0f;
            OnEnemyDeath();
        }
    }

    private void OnEnemyDeath()
    {
        Experience.Instance.DropExperience(this.experience, CollectExperience.Instance.experienceObject, transform.position);
        enemySpawner.totalEnemiesAlive--;
        transform.position = objectPool.transform.position;
        ItemDrop.Instance.RandomizeItemDropChance();
        gameObject.SetActive(false);
        if (enemySpawner.totalEnemiesAlive <= objectPool.enemyPoolSize)
        {
            Debug.Log("Total Enemies Alive:" + enemySpawner.totalEnemiesAlive);
            Debug.Log("ObjectPool Size:" + objectPool.enemyPoolSize);
            RandomizeEnemyType(enemySpawner.level, gameObject);
            gameObject.transform.position = enemySpawner.GetWorldPosition(enemySpawner.GetRandomGridPosition());
            gameObject.SetActive(true);
            enemySpawner.totalEnemiesAlive++;
        }
    }


    public void RandomizeEnemyType(int level, GameObject spawnedEnemy)
    {
        List<EnemyStats> enemies = enemyTypeHolder.GetEnemyList(level);

        if (enemies != null && enemies.Count > 0)
        {
            int randomIndex = Random.Range(0, enemies.Count);
            EnemyStats randomEnemy = enemies[randomIndex];
            Debug.Log("Selam");
            // Do something with the randomly selected EnemyStats
            spawnedEnemy.GetComponent<Enemy>().health = randomEnemy.health;
            spawnedEnemy.GetComponent<Enemy>().damage = randomEnemy.damage;
            spawnedEnemy.GetComponent<Enemy>().experience = randomEnemy.experience;
            Debug.Log("Test1");
            spawnedEnemy.GetComponent<SpriteRenderer>().sprite = randomEnemy.enemyPrefab.GetComponent<SpriteRenderer>().sprite;
            Debug.Log("Test2");
            spawnedEnemy.GetComponent<Enemy>().enemyName = randomEnemy.enemyName;
            Debug.Log("Randomly selected enemy: " + randomEnemy.enemyName);
        }
        else
        {
            Debug.LogWarning("No enemies found for level " + level);
        }
    }

}
