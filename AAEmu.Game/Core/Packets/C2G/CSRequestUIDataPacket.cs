using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.C2G;

public class CSRequestUIDataPacket() : GamePacket(CSOffsets.CSRequestUIDataPacket, 1)
{
    public override void Read(PacketStream stream)
    {
        var uiDataType = stream.ReadUInt16();
        var id = stream.ReadUInt64();

        if (Connection.Characters.TryGetValue((uint)id, out var value))
            Connection.SendPacket(
                new SCResponseUIDataPacket((uint)id, uiDataType, value.GetOption(uiDataType))
            );
    }
}
