using AAEmu.Commons.Network;
using AAEmu.Login.Core.Network.Login;

namespace AAEmu.Login.Core.Packets.C2L;

/// <summary>
/// Packet 0x17 sent by ArcheAge 10.8 client for token authentication.
/// </summary>
public class CARequestTokenAuthPacket() : LoginPacket(TypeId), ILoginPacket
{
    public new static ushort TypeId => CLOffsets.CARequestTokenAuthPacket;

    public uint MajorVersion { get; private set; }
    public uint MinorVersion { get; private set; }
    public string? Token { get; private set; }

    public override void Read(PacketStream stream)
    {
        MajorVersion = stream.ReadUInt32();
        MinorVersion = stream.ReadUInt32();
        var svc = stream.ReadByte();
        var dev = stream.ReadBoolean();
        var cpu = stream.ReadUInt64();
        var u1 = stream.ReadUInt16();
        var hwId = stream.ReadBytes();
        Token = stream.ReadString();
        var is64Bit = stream.ReadBoolean();
        var isMulti = stream.ReadBoolean();
        var serial = stream.ReadByte();
    }
}