using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;

namespace AAEmu.Game.Core.Packets.C2G;

public class CSSaveUIDataPacket() : GamePacket(CSOffsets.CSSaveUIDataPacket, 1)
{
    public override void Read(PacketStream stream)
    {
        var uiDataType = stream.ReadUInt16();
        var id = stream.ReadUInt64();
        var data = stream.ReadString();

        if (Connection.Characters.TryGetValue((uint)id, out var character))
        {
            character.SetOption(uiDataType, data);
            Logger.Debug($"[UI-DATA] Saved option {uiDataType} for char {id} (len={data.Length})");
        }
        else
        {
            Logger.Debug($"[UI-DATA] Received CSSaveUIDataPacket for unknown char {id}, option {uiDataType}");
        }
    }
}
