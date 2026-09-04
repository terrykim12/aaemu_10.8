using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using NLog;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCOnOffSnowPacket(bool on) : GamePacket(SCOffsets.SCOnOffSnowPacket, 1)
{
    private static readonly Logger Log = LogManager.GetCurrentClassLogger();

    public override PacketStream Write(PacketStream stream)
    {
        stream.Write(on);
        Log.Info("[10.8 WIRE] SCOnOffSnowPacket 0x{0:X3} written: on={1}, payloadLen=1", TypeId, on);
        return stream;
    }
}
