using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    public GameObject levelUpUIPanel;
    public Button[] buttons;

    private void Awake()
    {
        buttons = levelUpUIPanel.GetComponentsInChildren<Button>();
    }

    public void ShowUI()
    {
        levelUpUIPanel.SetActive(true);

        UpgradeType[] randomOptions = Enum.GetValues(typeof(UpgradeType)).OfType<UpgradeType>().OrderBy(x => UnityEngine.Random.value).Take(3).ToArray();

        for (int i=0; i<randomOptions.Length; i++)
        {
            UpgradeOption option = buttons[i].GetComponent<UpgradeOption>();
            option.upgradeType = randomOptions[i];
        }
    }

    public void HideUI() {
        levelUpUIPanel.SetActive(false);
    }
}
