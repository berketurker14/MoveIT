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

    private void Awake()
    {
        instance = this;
        enemyTypeHolder = EnemyTypeHolder.instance;
    }

    private void Start()
    {
        // Set up move directions for the enemy
        moveDirections = new Vector2Int[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left };
        MoveTowards(GridController.Instance.currentGridPosition);
        enemySpawner = EnemySpawner.instance;
        objectPool = ObjectPool.instance;
    }

    private void Update()
    {
        Move();
        UpdateHealth();
    }

    private void Move()
    {
        // Increment the turn timer
        turnTimer += Time.deltaTime;

        // If the turn timer is greater than the move time, move the enemy
        if (turnTimer >= turnTime)
        {
            // Reset the turn timer
            turnTimer = 0f;

            //transform.position = GridController.Instance.GetWorldPosition(targetGridPos);

            // Move the enemy
            MoveTowards(GridController.Instance.currentGridPosition);
        }


        // Increment the timer for the current move
        timer += Time.deltaTime;

        // Calculate the percentage of the move completed based on the timer and moveTime
        float t = Mathf.Clamp01(timer / moveTime);

        // Move towards the target grid position
        Vector3 targetPos = GridController.Instance.GetWorldPosition(targetGridPos);
        Vector3 direction = (targetPos - transform.position).normalized;
        Vector3 newPos = transform.position + direction * moveSpeed * Time.deltaTime;

        // Snap the new position to the grid
        Vector2Int newGridPos = new Vector2Int(Mathf.RoundToInt(newPos.x), Mathf.RoundToInt(newPos.y));

        // If the new grid position is different from the current grid position, move to the new grid position
        if (newGridPos != currentGridPos)
        {
            // Set the new grid position as the current grid position
            currentGridPos = targetGridPos;

            // Reset the timer for the new move
            timer = 0f;
        }

        // Interpolate between the current position and the target position based on the percentage of the move completed
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * moveSpeed);
    }

    public void MoveTowards(Vector2Int destinationGridPos)
    {
        Debug.Log("MoveTowards");
        if (destinationGridPos == currentGridPos)
        {
            // The player is on the same grid position as the enemy
            return;
        }
        // Get the direction to move in
        Vector2Int direction = destinationGridPos - currentGridPos;

        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Move in the x direction
            if (direction.x > 0)
            {
                // Move right
                targetGridPos = currentGridPos + Vector2Int.right;
            }
            else
            {
                // Move left
                targetGridPos = currentGridPos + Vector2Int.left;
            }
        }
        else
        {
            // Move in the y direction
            if (direction.y > 0)
            {
                // Move up
                targetGridPos = currentGridPos + Vector2Int.up;
            }
            else
            {
                // Move down
                targetGridPos = currentGridPos + Vector2Int.down;
            }
        }

        // Reset the timer for the new mov
        timer = 0f;
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
        ItemDrop.Instance.RandomizeItemDropChance(transform.position);
        enemySpawner.totalEnemiesAlive--;
        transform.position = objectPool.transform.position;
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
