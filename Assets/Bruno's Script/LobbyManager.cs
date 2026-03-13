using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager instance;

    public int maxPlayers = 4;

    private NetworkList<LobbyPlayer> lobbyPlayers;

    //DEBUG
    [SerializeField] private List<LobbyPlayer> debugLobbyPlayers = new();
    //

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        lobbyPlayers = new NetworkList<LobbyPlayer>();
        lobbyPlayers.OnListChanged += OnLobbyListChanged;

    }

    void Update()
    {
        //DEBUG
        debugLobbyPlayers.Clear();

        foreach (var p in lobbyPlayers)
        {
            debugLobbyPlayers.Add(p);
        }
        //
    }


    public int TotalPlayers => lobbyPlayers.Count;

    // Return the NetworkList itself
    public NetworkList<LobbyPlayer> LobbyPlayers => lobbyPlayers;

    /// <summary>
    /// Called by local client when a player wants to join (local player)
    /// </summary>
    public void RequestJoin(int localPlayerId)
    {
        if (!IsClient) return;

        JoinLobbyRpc(localPlayerId);
    }

    /// <summary>
    /// RPC: Server adds the player to the NetworkList
    /// </summary>
    [Rpc(SendTo.Server)]
    private void JoinLobbyRpc(int localPlayerId, RpcParams rpcParams = default)
    {
        if (!IsServer) return; // Only server modifies the NetworkList

        ulong clientId = rpcParams.Receive.SenderClientId;

        if (lobbyPlayers.Count >= maxPlayers)
            return;

        // Prevent duplicates (same local player on same client)
        foreach (var p in lobbyPlayers)
        {
            if (p.ClientId == clientId && p.LocalPlayerId == localPlayerId)
                return;
        }

        int slot = GetNextAvailableSlot();
        if (slot == -1) return;

        LobbyPlayer newPlayer = new LobbyPlayer(clientId, localPlayerId, slot);
        lobbyPlayers.Add(newPlayer);

        Debug.Log($"Client {clientId} Local {localPlayerId} joined slot {slot}");
    }

    /// <summary>
    /// Removes all players for a client (e.g., disconnect)
    /// </summary>
    public void RemoveLobbyPlayer(ulong clientId)
    {
        if (!IsServer) return;

        for (int i = lobbyPlayers.Count - 1; i >= 0; i--)
        {
            if (lobbyPlayers[i].ClientId == clientId)
            {
                lobbyPlayers.RemoveAt(i);
            }
        }

        Debug.Log($"Client {clientId} disconnected. Total players: {lobbyPlayers.Count}");
    }

    /// <summary>
    /// Returns first available slot (0..maxPlayers-1)
    /// </summary>
    private int GetNextAvailableSlot()
    {
        for (int i = 0; i < maxPlayers; i++)
        {
            bool used = false;
            foreach (var p in lobbyPlayers)
            {
                if (p.Slot == i)
                {
                    used = true;
                    break;
                }
            }
            if (!used) return i;
        }
        return -1;
    }

    /// <summary>
    /// Optional: UI / debug update when lobby changes
    /// </summary>
    private void OnLobbyListChanged(NetworkListEvent<LobbyPlayer> changeEvent)
    {
        Debug.Log($"Lobby changed: {changeEvent.Type}, total players: {lobbyPlayers.Count}");
    }
}