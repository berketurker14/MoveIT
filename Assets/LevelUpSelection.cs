using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpSelection : MonoBehaviour
{
    public Button[] GameObjectlevelUpButtons;
    private PlayerStats _playerStats; 
    private void Start()
    {
        Init();
        _playerStats = GridController.Instance.player.GetComponent<PlayerStats>();
    }

    private void Init()
    {
        GameObjectlevelUpButtons[0].onClick.AddListener(() => LevelUpStrength(1));
        GameObjectlevelUpButtons[1].onClick.AddListener(() => LevelUpIntelligence(1));
        GameObjectlevelUpButtons[2].onClick.AddListener(() => LevelUpDexterity(1));
    }

    public void LevelUpStrength(int strength)
    {
        _playerStats.strength += strength;
    }
    public void LevelUpIntelligence(int intelligence)
    {
        _playerStats.intelligence += intelligence;

    }
    public void LevelUpDexterity(int dexterity)
    {
        _playerStats.dexterity += dexterity;

    }
}
