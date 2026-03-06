[System.Serializable]
public class LobbyPlayer
{
    public ulong ClientId;
    public int PlayerIndex; // local index on that client (0,1,2...)
    public bool Ready;

    public LobbyPlayer(ulong clientId, int playerIndex)
    {
        ClientId = clientId;
        PlayerIndex = playerIndex;
        Ready = false;
    }
}