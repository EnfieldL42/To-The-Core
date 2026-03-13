using Unity.Netcode;
using System;

[Serializable]
public struct LobbyPlayer : INetworkSerializable, IEquatable<LobbyPlayer>
{
    public ulong ClientId;
    public int LocalPlayerId;
    public int Slot;

    public int CharacterId;
    public bool Ready;

    public LobbyPlayer(ulong clientId, int localPlayerId, int slot)
    {
        ClientId = clientId;
        LocalPlayerId = localPlayerId;
        Slot = slot;

        CharacterId = -1;
        Ready = false;
    }

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref ClientId);
        serializer.SerializeValue(ref LocalPlayerId);
        serializer.SerializeValue(ref Slot);
        serializer.SerializeValue(ref CharacterId);
        serializer.SerializeValue(ref Ready);
    }

    public bool Equals(LobbyPlayer other)
    {
        return ClientId == other.ClientId &&
               LocalPlayerId == other.LocalPlayerId &&
               Slot == other.Slot;
    }
}