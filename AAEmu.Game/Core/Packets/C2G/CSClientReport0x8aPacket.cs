using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.C2G;

/// <summary>
/// Packet 0x08A (level 5): Client periodic telemetry / heartbeat report sent every ~31 seconds.
/// </summary>
public class CSClientReport0x8aPacket : GamePacket
{
    public CSClientReport0x8aPacket() : base(CSOffsets.off_3A0FA8B0, 5)
    {
    }

    public override void Read(PacketStream stream)
    {
        // 1486-byte periodic telemetry / heartbeat data
        // Consumed cleanly to prevent unknown packet errors
    }
}
