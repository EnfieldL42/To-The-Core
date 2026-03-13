using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldInputManager : MonoBehaviour
{
    public static WorldInputManager instance;

    [SerializeField] private PlayerInputManager inputManager;
    [SerializeField] private List<PlayerInput> playersInputs = new();

    void Awake()
    {
       instance = this;
    }

    public void OnPlayerJoined(PlayerInput player)
    {
        if (!NetworkManager.Singleton.IsHost)
            DisablePlayerInputs();

        int localId = player.playerIndex;

        Debug.Log($"Local player joined: {localId}");

        LobbyManager.instance.RequestJoin(localId);
        playersInputs.Add(player);

    }

    public void PlayerDisconnected()
    {
        DisablePlayerInputs();
        RemoveAllPlayers();
    }

    private void RemoveAllPlayers()
    {
        foreach (var player in playersInputs)
        {
            player.user.UnpairDevicesAndRemoveUser();
            Destroy(player.gameObject);
        }

        playersInputs.Clear();
    }

    public void EnablePlayerInputs()
    {
        if (inputManager != null)
            inputManager.EnableJoining();
        else
            Debug.LogError("PlayerInputManager not assigned");
    }

    public void DisablePlayerInputs()
    {
        if (inputManager != null)
            inputManager.DisableJoining();
        else
            Debug.LogError("PlayerInputManager not assigned");
    }

}
