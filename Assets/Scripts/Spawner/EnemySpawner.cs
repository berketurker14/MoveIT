using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public EnemyStats[] enemyList;
    public GameObject player;
    private void Start()
    {
        SpawnEnemies();
    }
    public void SpawnEnemies()
    {
        for (int i = 0; i < 10; i++)
        {
            Vector2Int randomGridPosition = GetRandomGridPosition();
            GameObject enemyPrefab = GetRandomEnemyPrefab();
            Instantiate(enemyPrefab, GetWorldPosition(randomGridPosition), Quaternion.identity);
        }
    }

    private Vector2Int GetRandomGridPosition()
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

    private Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return GridController.Instance.GetWorldPosition(gridPosition);
    }
}
