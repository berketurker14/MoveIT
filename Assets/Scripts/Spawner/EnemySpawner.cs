using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyStats[] enemyList;
    public GameObject player;
    public float totalEnemiesAlive = 0;
    public int level = 0;
    [HideInInspector] public static EnemySpawner instance;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        for (int i = 0; i < 45; i++)
        {
            SpawnEnemy();
        }
    }
    public GameObject SpawnEnemy()
    {
        Vector2Int randomGridPosition = GetRandomGridPosition();
        GameObject enemyPrefab = GetRandomEnemyPrefab();
        GameObject spawnedEnemy = Instantiate(enemyPrefab, GetWorldPosition(randomGridPosition), Quaternion.identity);
        if (!spawnedEnemy.GetComponent<Enemy>())
        {
            spawnedEnemy.AddComponent<Enemy>();
            Enemy.instance.RandomizeEnemyType(level,spawnedEnemy);
        }
        totalEnemiesAlive++;
        Enemy.instance.RandomizeEnemyType(level, spawnedEnemy);
        return spawnedEnemy;
    }

    public Vector2Int GetRandomGridPosition()
    {
        Vector2Int playerGridPosition = GridController.Instance.GetGridPosition(player.transform.position);

        int x = playerGridPosition.x + Random.Range(-30, 31);
        int y = playerGridPosition.y + Random.Range(-10, 11);

        return new Vector2Int(x, y);
    }
    //Will be fixed after game design speech - Berke
    private GameObject GetRandomEnemyPrefab()
    {
        int randomIndex = Random.Range(0, enemyList.Length);
        return enemyList[randomIndex].enemyPrefab;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return GridController.Instance.GetWorldPosition(gridPosition);
    }
}
