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

    private void Start()
    {
        experienceObject = CollectExperience.Instance.experienceObject;
    }
    public void SetValue(float expValue)
    {
        value = expValue;
    }

    public float GetValue()
    {
        return value;
    }

    public void DropExperience(float expValue)
    {
        Vector3 targetPosition = new Vector3(GridController.Instance.currentGridPosition.x, GridController.Instance.currentGridPosition.y, 0f);
        Instantiate(experienceObject, targetPosition, Quaternion.identity);
        SetValue(expValue);
    }
}
