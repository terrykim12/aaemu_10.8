using System;

using AAEmu.Commons.Network;
using AAEmu.Game.Core.Network.Game;
using AAEmu.Game.Models.Game.Char;

namespace AAEmu.Game.Core.Packets.G2C;

public class SCCharacterStatePacket : GamePacket
{
    private readonly Character _character;

    public SCCharacterStatePacket(Character character) : base(SCOffsets.SCCharacterStatePacket, 5)
    {
        _character = character;
    }

    public override PacketStream Write(PacketStream stream)
    {
        // === Header (x2game.dll + 0x9C5850) ===
        stream.Write(_character.InstanceId); // iid (uint32, 4B)
        stream.Write(_character.Guid);       // guid (16B)
        stream.Write(0u);                    // rwd (uint32, 4B)
        stream.Write(0u);                    // srwd (uint32, 4B)

        // === Body (x2game.dll + 0x96F550) ===
        // 0x96F567: call 0x96b620 (CharacterInfo)
        _character.WriteState651723(stream);

        // 0x96F57A: angles (vec3, 12B)
        stream.Write(new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xDB, 0xFB, 0x17, 0xC0 });
        // 0x96F598: exp (uint32, 4B)
        stream.Write((uint)_character.Experience);
        // 0x96F5B6: heirExp (uint64, 8B)
        stream.Write(0UL);
        // 0x96F5D4: recoverableExp (uint32, 4B)
        stream.Write(_character.RecoverableExp);
        // 0x96F5F2: penaltiedExp (uint32, 4B)
        stream.Write(0u);
        // 0x96F610: returnDistrictId (uint32, 4B)
        stream.Write(0u);
        // 0x96F624: returnDistrict.type (int32, 4B)
        stream.Write(0);
        // 0x96F666: resurrectionDistrict.type (uint32, 4B)
        stream.Write(0u);

        // 0x96F6AB: abilityExp (0x1E = 30 entries)
        for (var i = 0; i < 30; i++)
        {
            stream.Write(0u);
        }

        // Mail block
        stream.Write(_character.Mails.UnreadMailCount.TotalSent);                // totalSentMail (0x96F6DE)
        stream.Write(_character.Mails.UnreadMailCount.TotalReceived);            // totalMail (0x96F6FF)
        stream.Write(_character.Mails.UnreadMailCount.TotalMiaReceived);         // totalMiaMail (0x96F71D)
        stream.Write(_character.Mails.UnreadMailCount.TotalCommercialReceived);  // totalCommercialMail (0x96F73B)
        stream.Write(_character.Mails.UnreadMailCount.UnreadReceived);           // unreadMail (0x96F759)
        stream.Write(_character.Mails.UnreadMailCount.UnreadMiaReceived);        // unreadMiaMail (0x96F777)
        stream.Write(_character.Mails.UnreadMailCount.UnreadCommercialReceived); // unreadCommercialMail (0x96F795)

        // Slot counts
        stream.Write((byte)_character.NumInventorySlots); // numInvenSlots (0x96F7B3: byte, 1B)
        stream.Write((short)_character.NumBankSlots);     // numBankSlots (0x96F7D1: short, 2B)

        // Money amounts (int64, 8B each)
        stream.Write(_character.Money);  // Inventory money (0x96F7EF)
        stream.Write(_character.Money2); // Bank money (0x96F80D)
        stream.Write(0L);                // moneyAmount (0x96F82B)
        stream.Write(0L);                // moneyAmount (0x96F849)

        stream.Write(_character.AutoUseAAPoint); // autoUseAAPoint (0x96F86E: byte, 1B)

        // 0x96F8AE: call 0x96f290 (EquipSlot reinforces map/vector size)
        stream.Write(0u); // size = 0

        stream.Write(0); // juryPoint (0x96F8C1: int32, 4B)
        stream.Write(0); // jailSeconds (0x96F8DF: int32, 4B)
        stream.Write(0); // reportedNo (0x96F8FD: int32, 4B)
        stream.Write(0); // suspectedNo (0x96F91B: int32, 4B)
        stream.Write(0); // totalPlayTime (0x96F939: int32, 4B)

        stream.Write(_character.ExpandedExpert); // expandedExpert (0x96F95E: byte, 1B)

        stream.Write((byte)0);  // remainBotCheckCnt (0x96F99E: byte, 1B)
        stream.Write((short)0); // failedBotCheckAccumCnt (0x96F9BF: short, 2B)

        // 0x96F9D6: instantTime (0xC = 12 entries)
        for (var i = 0; i < 12; i++)
        {
            stream.Write(0L);
        }

        stream.Write(0u);                // dailyLeadershipPoint (0x96FA0B: uint32, 4B)
        stream.Write(DateTime.MinValue); // lastDailyLeadershipPointTime (0x96FA2C: int64, 8B)
        stream.Write(0u);                // dailyHonorWarPoint (0x96FA47: uint32, 4B)
        stream.Write(0L);                // dailyHonorWarPointDate (0x96FA65: int64, 8B)
        stream.Write(0);                 // totalReportBadUser (0x96FA80: int32, 4B)
        stream.Write((byte)0);           // usableAbilSetSlotCount (0x96FA9E: byte, 1B)

        // Tail structures (0x96FAB9 - 0x96FC33)
        stream.Write(0u); // _pageInfos count; binary optional groups consume no bytes.
        stream.Write(0u);    // _selectPageIndex (0x96FADB: uint32, 4B)
        stream.Write(0);     // _extendMaxStats (0x96FAF8: int32, 4B)
        stream.Write(0);     // _applyExtendCount (0x96FB16: int32, 4B)
        stream.Write(0);     // type (0x96FB38: int32, 4B)
        stream.Write(0u); // appellationStamp: reader vtable+0xA0 consumes uint32.

        // equipSlotReinforces (0x96FB6A)
        stream.Write(0u); // equipSlotReinforces.slotInfoList count
        stream.Write(0u); // equipSlotReinforces.levelEffectList count

        stream.Write(false); // reservedQuestDropTarget (0x96FBC8: bool, 1B)
        stream.Write(0u);    // merchantGoodsLimitPurchaseMap size (0x96FBE2: uint32, 4B)
        stream.Write(0u);    // actSanctionMap size (0x96FBF9: uint32, 4B)
        stream.Write(0);     // additionalSkillPoint (0x96FC17: int32, 4B)

        return stream;
    }
}
