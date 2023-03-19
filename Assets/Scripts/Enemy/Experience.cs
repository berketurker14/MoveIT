using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience : MonoBehaviour
{
    public float value;
    [HideInInspector] public bool isCollected = false;
    [HideInInspector] public static Experience Instance;
    public GameObject experienceObject; // Object that drops when enemy dies

    private void Awake()
    {
        Instance = this;
    }


    public void SetValue(float expValue)
    {
        value = expValue;
    }

    public float GetValue()
    {
        return value;
    }

    public void DropExperience(float expValue, GameObject experienceObject, Vector3 spawnPosition)
    {
        Vector2Int gridPosition = GridController.Instance.GetGridPosition(spawnPosition);
        Vector3 alignedSpawnPosition = GridController.Instance.GetWorldPosition(gridPosition);
        GameObject temp = Instantiate(experienceObject, alignedSpawnPosition, Quaternion.identity);
        temp.GetComponent<Experience>().SetValue(expValue);
    }

}
