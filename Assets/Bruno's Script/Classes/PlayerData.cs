using System.Collections.Generic;

public class PlayerData
{
    public ulong ClientId;
    public int LocalPlayerId;
    public int Slot;

    public string Character;

    public bool IsConnected;

    public List<string> Upgrades = new();
}