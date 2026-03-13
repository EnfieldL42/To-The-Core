using Unity.Netcode;
using System;

[System.Serializable]
public struct LobbyPlayer : INetworkSerializable, IEquatable<LobbyPlayer>
{
    public ulong ClientId;      // Network client ID
    public int LocalPlayerId;   // Local player index on that client
    public int Slot;            // Slot in lobby

    public LobbyPlayer(ulong clientId, int localPlayerId, int slot)
    {
        ClientId = clientId;
        LocalPlayerId = localPlayerId;
        Slot = slot;
    }

    // Required for NetworkList syncing
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref LocalPlayerId);
        serializer.SerializeValue(ref Slot);
    }

    public bool Equals(LobbyPlayer other)
    {
        return ClientId == other.ClientId &&
               LocalPlayerId == other.LocalPlayerId &&
               Slot == other.Slot;
    }
}