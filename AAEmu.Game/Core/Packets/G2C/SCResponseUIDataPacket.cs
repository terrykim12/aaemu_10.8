using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCResponseUIDataPacket(uint characterId, ushort uiDataType, string uiData)
    : GamePacket(SCOffsets.SCResponseUIDataPacket, 1)
{
    public override PacketStream Write(PacketStream stream)
    {
        var data = uiData ?? string.Empty;
        stream.Write((ulong)characterId);
        stream.Write(uiDataType);
        stream.Write(data);
        stream.Write((uint)data.Length);
        return stream;
    }
}
