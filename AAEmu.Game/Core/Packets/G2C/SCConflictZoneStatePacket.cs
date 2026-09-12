using System;
using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.World.Zones;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCConflictZoneStatePacket : GamePacket
{
    private readonly ushort _zoneId;
    private readonly ZoneConflictType _hpws;
    private readonly DateTime _endTime;
    private readonly DateTime _lockTime;

    public SCConflictZoneStatePacket(ushort zoneId, ZoneConflictType hpws, DateTime endTime, DateTime lockTime = default) : base(SCOffsets.SCConflictZoneStatePacket, 5)
    {
        _zoneId = zoneId;
        _hpws = hpws;
        _endTime = endTime;
        _lockTime = lockTime;
    }

    public override PacketStream Write(PacketStream stream)
    {
        stream.Write(_zoneId);
        stream.Write((byte)_hpws);
        stream.Write(_endTime);
        stream.Write(_lockTime);
        return stream;
    }
}
