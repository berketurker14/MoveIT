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
        _playerStats = PlayerStats.Instance;
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
        Time.timeScale = 1f;
        CollectExperience.Instance.levelUpMenu.SetActive(false);
    }
    public void LevelUpIntelligence(int intelligence)
    {
        _playerStats.intelligence += intelligence;
        Time.timeScale = 1f;
        CollectExperience.Instance.levelUpMenu.SetActive(false);
    }
    public void LevelUpDexterity(int dexterity)
    {
        _playerStats.dexterity += dexterity;
        Time.timeScale = 1f;
        CollectExperience.Instance.levelUpMenu.SetActive(false);
    }
}
