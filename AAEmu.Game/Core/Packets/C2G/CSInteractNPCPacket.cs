using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Core.Packets.G2C;

namespace AAEmu.Game.Core.Packets.C2G;

public class CSInteractNPCPacket() : GamePacket(CSOffsets.CSInteractNPCPacket, 1)
{
    public override void Read(PacketStream stream)
    {
        var objId = stream.ReadBc();
        var isTargetChanged = stream.ReadBoolean();

        Logger.Debug("InteractNPC, BcId: {0}, TargetChanged: {1}", objId, isTargetChanged);

        var unit = objId > 0 ? Connection.ActiveChar.ParentWorld.GetUnit(objId) : null;

        Connection.ActiveChar.CurrentInteractionObject = unit;

        if (isTargetChanged)
        {
            Connection.ActiveChar.CurrentTarget = unit;
        }

        // Note: SCAiAggroPacket opcode 0x23F in 10.8 is invalid/mismatched and causes client sc error.
        // Connection.SendPacket(new SCAiAggroPacket(objId, 0));
    }
}
