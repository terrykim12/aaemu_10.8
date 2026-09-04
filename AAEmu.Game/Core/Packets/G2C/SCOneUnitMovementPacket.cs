using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Units.Movements;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCOneUnitMovementPacket(uint id, MoveType type, byte extraFlags = 0)
    : GamePacket(SCOffsets.SCOneUnitMovementPacket, 1) // TODO ... SCUnitMovementsPacket
{
    public override PacketLogLevel LogLevel => PacketLogLevel.Off;

    public override PacketStream Write(PacketStream stream)
    {
        stream.WriteBc(id);
        stream.Write((byte)type.Type);
        stream.Write(type);
        stream.Write(extraFlags);
        return stream;
    }

    public override string Verbose()
    {
        return " - " + (type?.Type.ToString() ?? "none") + " " + (Connection.ActiveChar?.ParentWorld?.GetGameObject(id)?.DebugName() ?? "(" + id + ")");
    }
}
