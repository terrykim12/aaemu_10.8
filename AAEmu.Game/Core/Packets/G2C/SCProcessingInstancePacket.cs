using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using NLog;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCProcessingInstancePacket(int zoneId, int state = 0) : GamePacket(SCOffsets.SCProcessingInstancePacket, 1)
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public override PacketStream Write(PacketStream stream)
    {
        stream.Write(zoneId);
        stream.Write(state);
        Log.Info("[10.8 WIRE] SCProcessingInstancePacket 0x{0:X3} written: zoneId={1}, state={2}, payloadLen=8",
            TypeId, zoneId, state);
        return stream;
    }
}
