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


    public int TotalPlayers => lobbyPlayers.Count;

    // Return the NetworkList itself
    public NetworkList<LobbyPlayer> LobbyPlayers => lobbyPlayers;

    /// <summary>
    /// Called by local client when a player wants to join (local player)
    /// </summary>
    public void RequestJoin(int localPlayerId)
    {
        if (!IsClient) return;

        JoinLobbyRpc();
    }

    /// <summary>
    /// RPC: Server adds the player to the NetworkList
    /// </summary>
    [Rpc(SendTo.Server)]
    private void JoinLobbyRpc(RpcParams rpcParams = default)
    {
        if (!IsServer) return;

        ulong clientId = rpcParams.Receive.SenderClientId;

        if (lobbyPlayers.Count >= maxPlayers)
            return;

        bool isHost = clientId == NetworkManager.ServerClientId;

        // Count players from this client
        int playersFromClient = 0;
        foreach (var p in lobbyPlayers)
        {
            if (p.ClientId == clientId)
                playersFromClient++;
        }

        // Non-host clients can only have one player
        if (!isHost && playersFromClient >= 1)
            return;

        int slot = GetNextAvailableSlot();
        if (slot == -1) return;

        // Server assigns LocalPlayerId
        int localPlayerId = playersFromClient;

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
        bool[] usedSlots = new bool[maxPlayers];

        foreach (var p in lobbyPlayers)
        {
            if (p.Slot >= 0 && p.Slot < maxPlayers)
                usedSlots[p.Slot] = true;
        }

        for (int i = 0; i < maxPlayers; i++)
        {
            if (!usedSlots[i])
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Optional: UI / debug update when lobby changes
    /// </summary>
    private void OnLobbyListChanged(NetworkListEvent<LobbyPlayer> changeEvent)
    {
        debugLobbyPlayers.Clear();

        foreach (var p in lobbyPlayers)
            debugLobbyPlayers.Add(p);

        Debug.Log($"Lobby changed: {changeEvent.Type}, total players: {lobbyPlayers.Count}");
    }
}