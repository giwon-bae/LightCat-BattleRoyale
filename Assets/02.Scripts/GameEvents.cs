using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public static class GameEvents
{
    public static event Action OnJoinLobbyRequest;

    public static event Action<GameMode, string> OnJoinGameRequest;

    public static void InvokeJoinLobbyRequest()
    {
        OnJoinLobbyRequest?.Invoke();
    }

    public static void InvokeJoinGameRequest(GameMode mode, string sessionName) {
        OnJoinGameRequest?.Invoke(mode, sessionName);
    }
}
