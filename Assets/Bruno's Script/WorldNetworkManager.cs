using Unity.Netcode;
using UnityEngine;

public class WorldNetworkManager : MonoBehaviour
{
    public static WorldNetworkManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void RegisterCallbacks()
    {
        var nm = NetworkManager.Singleton;

        nm.OnClientConnectedCallback -= OnClientConnected;
        nm.OnClientDisconnectCallback -= OnClientDisconnect;

        nm.OnClientConnectedCallback += OnClientConnected;
        nm.OnClientDisconnectCallback += OnClientDisconnect;
    }

    public void StartAsHost()
    {
        RegisterCallbacks();
        NetworkManager.Singleton.StartHost();
    }

    public void StartAsClient()
    {
        RegisterCallbacks();
        NetworkManager.Singleton.StartClient();
    }

    private void OnClientConnected(ulong clientId)
    {
        if (clientId != NetworkManager.Singleton.LocalClientId) return;

        if (WorldInputManager.instance != null)
            WorldInputManager.instance.EnablePlayerInputs();
    }

    private void OnClientDisconnect(ulong clientId)
    {
        if (LobbyManager.instance != null)
        {
            LobbyManager.instance.RemoveLobbyPlayer(clientId);
            WorldInputManager.instance.PlayerDisconnected();
        }
    }

    void OnDestroy()
    {
        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnect;
    }
}