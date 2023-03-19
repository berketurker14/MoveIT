using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    public GameObject[] items;
    public string[] rarities = { "Common", "Uncommon", "Rare", "Epic" };
    public static ItemDrop Instance;

    private void Awake()
    {
        Instance = this;
    }
    public void RandomizeItemDropChance(Vector3 enemyPos)
    {
        int chance = Random.Range(1, 66); // Roll a 1/65 chance
        if (chance == 1)
        {
            RandomizeRarity(enemyPos); // If item is dropped, determine its rarity
        }
    }

    public void RandomizeRarity(Vector3 spawnPosition)
    {
        int rarityIndex = Random.Range(0, rarities.Length); // Pick a random rarity
        string rarity = rarities[rarityIndex];

        // Filter items based on the selected rarity
        List<GameObject> filteredItems = new List<GameObject>();
        foreach (GameObject item in items)
        {
            ItemProperties itemProperties = item.GetComponent<ItemProperties>();
            if (itemProperties != null && itemProperties.rarity == rarity)
            {
                filteredItems.Add(item);
            }
        }

        if (filteredItems.Count > 0)
        {
            Vector2Int gridPosition = GridController.Instance.GetGridPosition(spawnPosition);
            Vector3 alignedSpawnPosition = GridController.Instance.GetWorldPosition(gridPosition);
            // Drop a random item of the selected rarity
            int itemIndex = Random.Range(0, filteredItems.Count);
            GameObject itemToDrop = filteredItems[itemIndex];
            Instantiate(itemToDrop, alignedSpawnPosition, Quaternion.identity);
        }
    }
}