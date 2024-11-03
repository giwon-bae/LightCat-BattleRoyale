using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public LevelUpManager levelUpManager;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ShowLevelUpUI()
    {
        levelUpManager.ShowUI();
    }

    public void HideLevelUpUI()
    {
        levelUpManager.HideUI();
    }
}
