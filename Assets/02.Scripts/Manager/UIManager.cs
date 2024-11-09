using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject _sessionButtonPrefab;
    [SerializeField] private Transform _sessionListContent;

    private Dictionary<string, GameObject> _sessionButtons = new Dictionary<string, GameObject>();

    public void UpdateSessionList(List<SessionInfo> sessionList)
    {
        ClearSessionList();

        foreach(var info in sessionList)
        {
            CreateSessionButton(info);
        }

        LayoutRebuilder.ForceRebuildLayoutImmediate(_sessionListContent as RectTransform);
    }

    private void ClearSessionList()
    {
        foreach(var button in _sessionButtons.Values)
        {
            Destroy(button);
        }
        _sessionButtons.Clear();
    }

    private void CreateSessionButton(SessionInfo info)
    {
        GameObject newButton = Instantiate(_sessionButtonPrefab, _sessionListContent);

        string sessionText = $"{info.Name} - ({info.PlayerCount}/{info.MaxPlayers})";
        TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        if (buttonText != null)
        {
            buttonText.text = sessionText;
        }

        _sessionButtons[info.Name] = newButton;
    }
}
