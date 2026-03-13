using UnityEngine;
using UnityEngine.InputSystem;

public class WorldInputManager : MonoBehaviour
{
    public static WorldInputManager instance;
    [SerializeField] private PlayerInputManager inputManager;

    void Awake()
    {
       instance = this;
    }

    public void OnPlayerJoined(PlayerInput input)
    {
        int localId = input.playerIndex;

        Debug.Log($"Local player joined: {localId}");

        LobbyManager.instance.RequestJoin(localId);
    }

    public void EnablePlayerInputs()
    {
        if (inputManager != null)
            inputManager.EnableJoining();
        else
            Debug.LogError("PlayerInputManager not assigned");
    }
}
