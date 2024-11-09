using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Fusion;
using Fusion.Sockets;
using System;
using UnityEngine.SceneManagement;

public class BasicSpawner : MonoBehaviour, INetworkRunnerCallbacks
{
    private NetworkRunner _runner;

    [SerializeField] private NetworkPrefabRef _playerPrefab;
    private Dictionary<PlayerRef, NetworkObject> _spawnedCharacters = new Dictionary<PlayerRef, NetworkObject>();

    //private bool _isLobby = false;

    [SerializeField] private UIManager _uiManager;

    void Awake()
    {
        // Create the Fusion runner and let it know that we will be providing user input
        _runner = gameObject.AddComponent<NetworkRunner>();
        _runner.ProvideInput = true;

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        GameEvents.OnJoinLobbyRequest += HandleJoinLobbyRequest;
        GameEvents.OnJoinGameRequest += HandleJoinGameRequest;
    }

    private void OnDisable()
    {
        GameEvents.OnJoinLobbyRequest -= HandleJoinLobbyRequest;
        GameEvents.OnJoinGameRequest -= HandleJoinGameRequest;
    }

    async Task StartGame(GameMode mode, string sessionName)
    {
        // Create the NetworkSceneInfo from the current scene
        var scene = SceneRef.FromIndex(SceneManager.GetActiveScene().buildIndex);
        var sceneInfo = new NetworkSceneInfo();
        if (scene.IsValid)
        {
            sceneInfo.AddSceneRef(scene, LoadSceneMode.Additive);
        }

        // Start or join (depends on gamemode) a session with a specific name
        var result = await _runner.StartGame(new StartGameArgs()
        {
            GameMode = mode,
            SessionName = sessionName,
            Scene = scene,
            SceneManager = gameObject.AddComponent<NetworkSceneManagerDefault>()
        });

        if (!result.Ok)
        {
            Debug.LogError("Failed to start game");
        }
        else
        {
            Debug.Log("Success to start game");
            _uiManager.HideAllPanel();
        }
    }

    private void OnGUI()
    {
        //if (_isLobby)
        //{
        //    if (GUI.Button(new Rect(0, 0, 200, 40), "Host"))
        //    {
        //        StartGame(GameMode.Host);
        //    }
        //    if (GUI.Button(new Rect(0, 40, 200, 40), "Join"))
        //    {
        //        StartGame(GameMode.Client);
        //    }
        //}
        //else
        //{
        //    if (GUI.Button(new Rect(0,0,200,40), "Find Game"))
        //    {
        //        var joinLobbyTask = JoinLobby();
        //    }
        //}
    }

    private async void HandleJoinGameRequest(GameMode mode, string sessionName)
    {
        await StartGame(mode, sessionName);
    }

    private async void HandleJoinLobbyRequest()
    {
        await JoinLobby();
    }

    private async Task JoinLobby()
    {
        var result = await _runner.JoinSessionLobby(SessionLobby.Custom, "TmpLobby");

        if (!result.Ok)
        {
            Debug.LogError("Failed to join lobby");
        }
        else
        {
            Debug.Log("Success to join lobby");
            //_isLobby = true;
            _uiManager.ShowSessionListPanel();
        }
    }

    #region INetworkRunnerCallbacks
    public void OnConnectedToServer(NetworkRunner runner) { }

    public void OnConnectFailed(NetworkRunner runner, NetAddress remoteAddress, NetConnectFailedReason reason) { }

    public void OnConnectRequest(NetworkRunner runner, NetworkRunnerCallbackArgs.ConnectRequest request, byte[] token) { }

    public void OnCustomAuthenticationResponse(NetworkRunner runner, Dictionary<string, object> data) { }

    public void OnDisconnectedFromServer(NetworkRunner runner, NetDisconnectReason reason) { }

    public void OnHostMigration(NetworkRunner runner, HostMigrationToken hostMigrationToken) { }

    public void OnInput(NetworkRunner runner, NetworkInput input) {
        var data = new NetworkInputData
        {
            direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized,
            attackInput = Input.GetButton("Fire1"),
            skillInput = Input.GetButton("Fire2")
        };

        input.Set(data);
    }

    public void OnInputMissing(NetworkRunner runner, PlayerRef player, NetworkInput input) { }

    public void OnObjectEnterAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    public void OnObjectExitAOI(NetworkRunner runner, NetworkObject obj, PlayerRef player) { }

    public void OnPlayerJoined(NetworkRunner runner, PlayerRef player)
    {
        if (runner.IsServer)
        {
            // Create a unique position for the player
            Vector3 spawnPosition = new Vector3((player.RawEncoded % runner.Config.Simulation.PlayerCount) * 3, 1, 0);
            NetworkObject networkPlayerObject = runner.Spawn(_playerPrefab, spawnPosition, Quaternion.identity, player);
            // Keep track of the player avatars for easy access
            _spawnedCharacters.Add(player, networkPlayerObject);
        }
    }

    public void OnPlayerLeft(NetworkRunner runner, PlayerRef player)
    {
        if (_spawnedCharacters.TryGetValue(player, out NetworkObject networkObject))
        {
            runner.Despawn(networkObject);
            _spawnedCharacters.Remove(player);
        }
    }

    public void OnReliableDataProgress(NetworkRunner runner, PlayerRef player, ReliableKey key, float progress) { }

    public void OnReliableDataReceived(NetworkRunner runner, PlayerRef player, ReliableKey key, ArraySegment<byte> data) { }

    public void OnSceneLoadDone(NetworkRunner runner) { }

    public void OnSceneLoadStart(NetworkRunner runner) { }

    public void OnSessionListUpdated(NetworkRunner runner, List<SessionInfo> sessionList) {
        _uiManager.UpdateSessionList(sessionList);
    }

    public void OnShutdown(NetworkRunner runner, ShutdownReason shutdownReason) { }

    public void OnUserSimulationMessage(NetworkRunner runner, SimulationMessagePtr message) { }
    #endregion
}
