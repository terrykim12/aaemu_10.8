using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Quests;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCCompletedQuestsPacket(CompletedQuest[] quests) : GamePacket(SCOffsets.SCCompletedQuestsPacket, 1)
{
    public override PacketStream Write(PacketStream stream)
    {
        stream.Write(quests.Length); // TODO max 200
        foreach (var quest in quests)
        {
            var body = new byte[8];
            quest.Body.CopyTo(body, 0);

            var blockIndex = (uint)quest.Id;
            stream.Write(blockIndex); // idx (int32, 4 bytes)
            stream.Write(body); // body (8 bytes)
        }
        return stream;
    }
}
