using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager Instance;

    [SerializeField] private int maxPlayers = 4;

    // tracks how many local players each client has
    //private Dictionary<ulong, int> clientPlayerCounts = new();

    [SerializeField] private List<LobbyPlayer> lobbyPlayers = new List<LobbyPlayer>();


    private void Awake()
    {
        Instance = this;
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            NetworkManager.OnClientDisconnectCallback += OnClientDisconnect;
        }
    }

    public void StartNetworkAsHost()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.StartHost();
    }

    public void StartNetworkAsClient()
    {
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.StartClient();
    }

    // called by UI when a player presses join
    public void RequestJoin()
    {
        if (!IsClient) return;

        JoinLobbyRpc();
    }

    [Rpc(SendTo.Server)]
    private void JoinLobbyRpc(RpcParams rpcParams = default)
    {
        ulong clientId = rpcParams.Receive.SenderClientId;

        if (lobbyPlayers.Count >= maxPlayers)
        {
            Debug.Log("Lobby full.");
            return;
        }

        int playerIndex = 0;

        foreach (var p in lobbyPlayers)
            if (p.ClientId == clientId)
                playerIndex++;

        LobbyPlayer newPlayer = new LobbyPlayer(clientId, playerIndex);

        lobbyPlayers.Add(newPlayer);

        Debug.Log($"Client {clientId} added player {playerIndex}");
        Debug.Log($"Total players: {lobbyPlayers.Count}");
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (!IsServer) return;

        lobbyPlayers.RemoveAll(p => p.ClientId == clientId);

        Debug.Log($"Client {clientId} disconnected");
        Debug.Log($"Total players: {lobbyPlayers.Count}");
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;

        RequestJoin();
    }

    public List<LobbyPlayer> GetLobbyPlayers()
    {
        return lobbyPlayers;
    }
}