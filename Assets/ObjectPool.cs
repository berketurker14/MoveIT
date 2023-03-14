using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;

    public GameObject enemyPrefab;
    public GameObject itemPrefab;

    // Set pool size for enemies to 45
    public int enemyPoolSize = 30;

    // Set pool size for items to 1
    public int itemPoolSize = 1;

    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    private Queue<GameObject> itemPool = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        //for (int i = 0; i < enemyPoolSize; i++)
        //{
        //    GameObject enemyObj = Instantiate(enemyPrefab);
        //    enemyObj.SetActive(false);
        //    enemyPool.Enqueue(enemyObj);
        //}

        //for (int i = 0; i < itemPoolSize; i++)
        //{
        //    GameObject itemObj = Instantiate(itemPrefab);
        //    itemObj.SetActive(false);
        //    itemPool.Enqueue(itemObj);
        //}
    }

    public GameObject GetEnemy()
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemyObj = enemyPool.Dequeue();
            enemyObj.SetActive(true);
            return enemyObj;
        }
        else
        {
            GameObject enemyObj = Instantiate(enemyPrefab);
            return enemyObj;
        }
    }

    public GameObject GetItem()
    {
        if (itemPool.Count > 0)
        {
            GameObject itemObj = itemPool.Dequeue();
            itemObj.SetActive(true);
            return itemObj;
        }
        else
        {
            GameObject itemObj = Instantiate(itemPrefab);
            return itemObj;
        }
    }

    public void ReturnEnemy(GameObject enemyObj)
    {
        enemyObj.SetActive(false);
        enemyPool.Enqueue(enemyObj);
    }

    public void ReturnItem(GameObject itemObj)
    {
        itemObj.SetActive(false);
        itemPool.Enqueue(itemObj);
    }
}
