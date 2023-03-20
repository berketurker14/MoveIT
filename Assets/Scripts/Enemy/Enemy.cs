using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2Int currentGridPos;
    public Vector2Int targetGridPos;
    public Vector2Int[] moveDirections;
    public float moveTime = 0.5f;
    private float turnTimer = 0f;
    public float turnTime = 1f;
    private float timer;
    public float health, damage, experience;
    public string enemyName;
    public GameObject enemyPrefab;
    [HideInInspector] public ObjectPool objectPool;
    [HideInInspector] public EnemySpawner enemySpawner;
    public EnemyTypeHolder enemyTypeHolder;
    public static Enemy instance;
    public float checkRadius = 0.5f;
    public int gridObstacleLayer;
    private bool isInitialMove = true;

    private void Awake()
    {
        instance = this;
        enemyTypeHolder = EnemyTypeHolder.instance;
        gridObstacleLayer = 0;
    }

    private void Start()
    {
        moveDirections = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        enemySpawner = EnemySpawner.instance;
        objectPool = ObjectPool.instance;
        currentGridPos = GridController.Instance.GetGridPosition(transform.position);
        turnTime = Random.Range(0.3f, 1.7f);
        StartCoroutine(MoveRoutine());
    }

    private void Update()
    {
        Move();
    }

    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            MoveTowards(GridController.Instance.GetGridPosition(GridController.Instance.player.transform.position));
            UpdateHealth();
            yield return new WaitForSeconds(turnTime); // Wait for 1 second before calling Move() again
        }
    }

    private void Move()
    {
        turnTimer += Time.deltaTime;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / moveTime);

        Vector3 targetPos = GridController.Instance.GetWorldPosition(targetGridPos);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;

        Vector2Int newGridPos = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        if (newGridPos != currentGridPos)
        {
            currentGridPos = targetGridPos;
            timer = 0f;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }
    public void MoveTowards(Vector2Int destinationGridPos)
    {
        if (isInitialMove)
        {
            ChooseRandomDirection();
            isInitialMove = false;
            return;
        }

        if (destinationGridPos == currentGridPos)
        {
            return;
        }
        Vector2Int direction = destinationGridPos - currentGridPos;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                targetGridPos = currentGridPos + Vector2Int.right;
            }
            else
            {
                targetGridPos = currentGridPos + Vector2Int.left;
            }
        }
        else
        {
            if (direction.y > 0)
            {
                targetGridPos = currentGridPos + Vector2Int.up;
            }
            else
            {
                targetGridPos = currentGridPos + Vector2Int.down;
            }
        }

        if (!GridController.Instance.IsGridPositionAvailable(targetGridPos))
        {
            ChooseRandomDirection();
        }

        GridController.Instance.SetGridPositionOccupied(currentGridPos, false);
        GridController.Instance.SetGridPositionOccupied(targetGridPos, true);

        timer = 0f;
    }
    // Ekrandaki render olaylarını kontrol et
    private bool IsGridPositionAvailable(Vector2Int gridPos)
    {
        Vector2 worldPos = GridController.Instance.GetWorldPosition(gridPos);
        Vector2 boxSize = new Vector2(checkRadius * 2, checkRadius * 2);
        Collider2D collider = Physics2D.OverlapBox(worldPos, boxSize, 0f, gridObstacleLayer);
        Debug.DrawLine(transform.position, worldPos, Color.red, 1f);
        return collider == null;
    }

    private void ChooseRandomDirection()
    {
        int randomPositionTryCount = 0;
        Vector2Int randomDir;
        do
        {
            if (randomPositionTryCount > 4)
            {
                Debug.LogError("Could not find a random position after 4 tries");
                targetGridPos = currentGridPos;
                return;
            }
            randomPositionTryCount++;
            randomDir = moveDirections[Random.Range(0, moveDirections.Length)];
            targetGridPos = currentGridPos + randomDir;
        } while (!GridController.Instance.IsGridPositionAvailable(targetGridPos));
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
