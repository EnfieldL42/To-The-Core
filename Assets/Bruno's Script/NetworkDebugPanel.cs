using Unity.Netcode;
using UnityEngine;

public class NetworkDebugPanel : MonoBehaviour
{
    [SerializeField] private bool showPanel = true;
    private Vector2 scrollPosition;

    private void OnGUI()
    {
        if (!showPanel || LobbyManager.instance == null) return;

        // Panel background
        GUI.Box(new Rect(10, 10, 300, 200), "Lobby Debug");

        GUILayout.BeginArea(new Rect(15, 35, 290, 160));

        // Scrollable area in case many players
        scrollPosition = GUILayout.BeginScrollView(scrollPosition);

        GUILayout.Label($"Total Players: {LobbyManager.instance.TotalPlayers}/{LobbyManager.instance.maxPlayers}");

        foreach (var player in LobbyManager.instance.LobbyPlayers)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label($"Client: {player.ClientId}", GUILayout.Width(90));
            GUILayout.Label($"Local: {player.LocalPlayerId}", GUILayout.Width(60));
            GUILayout.Label($"Slot: {player.Slot}", GUILayout.Width(50));
            GUILayout.EndHorizontal();
        }

        GUILayout.EndScrollView();
        GUILayout.EndArea();
    }
}