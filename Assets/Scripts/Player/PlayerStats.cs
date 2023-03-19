using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;
    private void Awake()
    {
        Instance = this;
    }
    public float health = 100f;
    public float strength = 10f;
    public float dexterity = 10f;
    public float intelligence = 10f;
}
