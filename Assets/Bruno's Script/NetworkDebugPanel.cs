using Unity.Netcode;
using UnityEngine;

public class NetworkDebugPanel : MonoBehaviour
{
    void OnGUI()
    {
        if (!NetworkManager.Singleton) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 500), GUI.skin.box);

        GUILayout.Label("=== NETWORK DEBUG ===");

        GUILayout.Label($"IsHost: {NetworkManager.Singleton.IsHost}");
        GUILayout.Label($"IsServer: {NetworkManager.Singleton.IsServer}");
        GUILayout.Label($"IsClient: {NetworkManager.Singleton.IsClient}");

        GUILayout.Space(10);

        GUILayout.Label($"Local Client ID: {NetworkManager.Singleton.LocalClientId}");

        GUILayout.Space(10);

        GUILayout.Label("Connected Clients:");

        foreach (var client in NetworkManager.Singleton.ConnectedClients)
        {
            GUILayout.Label($"ClientID: {client.Key}");
        }

        GUILayout.Space(10);
        GUILayout.Label("Lobby Players:");

        foreach (var p in LobbyManager.Instance.GetLobbyPlayers())
        {
            GUILayout.Label($"Client {p.ClientId} Player {p.PlayerIndex}");
        }

        GUILayout.EndArea();

    }
}