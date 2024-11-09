using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fusion;
using TMPro;

public class UIManager : MonoBehaviour
{
    [Header("Find Game")]
    [SerializeField] private GameObject _findGamePanel;
    [SerializeField] private Button _findGameButton;

    [Header("Session List")]
    [SerializeField] private GameObject _sessionListPanel;
    [SerializeField] private GameObject _sessionButtonPrefab;
    [SerializeField] private Transform _sessionListContent;
    [SerializeField] private TMP_InputField _sessionInput;
    [SerializeField] private Button _createGameButton;

    //[Header("Create Game")]
    //[SerializeField] private GameObject _createGamePanel;
    //[SerializeField] private TMP_InputField _sessionInput;
    //[SerializeField] private Button _createGameButton;


    private Dictionary<string, GameObject> _sessionButtons = new Dictionary<string, GameObject>();

    private void Awake()
    {
        _findGamePanel.SetActive(true);
        _sessionListPanel.SetActive(false);
        //_createGamePanel.SetActive(false);

        if (_findGameButton != null) {
            _findGameButton.onClick.AddListener(OnFindGameButtonClicked);
        }
        if (_createGameButton != null)
        {
            _createGameButton.onClick.AddListener(OnCreateGameButtonClicked);
        }
    }

    public void HideAllPanel() {
        _findGamePanel.SetActive(false);
        _sessionListPanel.SetActive(false);
    }

    #region FindGame
    private void OnFindGameButtonClicked()
    {
        GameEvents.InvokeJoinLobbyRequest();
    }
    #endregion

    #region SessionList
    private void OnCreateGameButtonClicked()
    {
        if (_sessionInput == null || _sessionInput.text == "") return;

        GameEvents.InvokeJoinGameRequest(GameMode.Host, _sessionInput.text);
    }

    public void ShowSessionListPanel()
    {
        _findGamePanel.SetActive(false);
        _sessionListPanel.SetActive(true);
        //_createGamePanel.SetActive(false);
    }

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
        GameObject newButtonObj = Instantiate(_sessionButtonPrefab, _sessionListContent);

        string sessionText = $"{info.Name} - ({info.PlayerCount}/{info.MaxPlayers})";
        TextMeshProUGUI buttonText = newButtonObj.GetComponentInChildren<TextMeshProUGUI>();
        Button button = newButtonObj.GetComponent<Button>();
        if (buttonText != null)
        {
            buttonText.text = sessionText;
            button.onClick.AddListener(() => { GameEvents.InvokeJoinGameRequest(GameMode.Client, info.Name); });
        }

        _sessionButtons[info.Name] = newButtonObj;
    }
    #endregion
}
