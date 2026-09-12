using System;
using AAEmu.Game.Core.Managers.UnitManagers;
using AAEmu.Game.Models.Game.Char;
using AAEmu.Commons.Network;
using AAEmu.Game.Models.Game.Units;

namespace AAEmu.Game.Models.Game.Items;

// 10.8 equipment block (LobbyChar_WriteEquipment). Shared by SC_PACKET_UNIT_STATE
// (called with the unit's idType) and the character-list lobby record (called with mode 0 = Character).
// Wire: validFlags u64 (bit i set iff slot i is occupied, over 35 slots) + each occupied slot in order
// (empty slots emit nothing) + — for a Character — a trailing per-slot flags u64. Per-slot form depends on
// the unit type and slot range: body-image slots 19-25 (non-Slave) write templateId only; an Npc writes a
// compact {templateId, id, grade} for normal slots and a full item for 27/31-33; a Character writes the
// full 10.8 item record (x2game.dll RVA 0x9686e0); everything else writes a full legacy item.
public static class EquipmentSerializer
{
    private const int SlotCount = 35; // 10.8 equip-slot count (x2game.dll 0x96ad7f: cmp edi, 0x23)

    public static void Write(PacketStream stream, Unit unit, BaseUnitType baseUnitType)
    {
        // Face/body meshes are template-only records on this wire. Preserve equipped
        // choices; project GameData defaults only for missing base meshes, without
        // creating inventory objects or modifying stored items.
        var character = unit as Character;
        var template = character != null && baseUnitType == BaseUnitType.Character
            ? CharacterManager.Instance.GetTemplate((byte)character.Race, (byte)character.Gender)
            : null;
        if (template?.ModelId != unit.ModelId)
            template = null;
        uint MissingBaseMesh(int slot) => slot switch
        {
            19 => template?.DefaultFaceItemId ?? 0,
            20 => template?.DefaultHairItemId ?? 0,
            24 => template?.DefaultBodyItemId ?? 0,
            _ => 0
        };
        ulong validFlags = 0;
        for (var i = 0; i < SlotCount; i++)
        {
            if (unit.Equipment.GetItemBySlot(i) != null || MissingBaseMesh(i) != 0)
                validFlags |= 1UL << i;
        }
        stream.Write(validFlags);

        if (validFlags == 0 && baseUnitType == BaseUnitType.Npc)
            unit.ModelParams.SetType(UnitCustomModelType.Skin); // NPC with no body and no face

        for (var i = 0; i < SlotCount; i++)
        {
            var item = unit.Equipment.GetItemBySlot(i);
            if (item == null)
            {
                var baseMesh = MissingBaseMesh(i);
                if (baseMesh != 0)
                    stream.Write(baseMesh);
                continue;
            }

            if (i is >= 19 and <= 25 && baseUnitType != BaseUnitType.Slave)
            {
                stream.Write(item.TemplateId); // body-image slots: templateId only (0x96ac5a)
            }
            else if (baseUnitType == BaseUnitType.Npc)
            {
                if (i == 27 || i is >= 31 and <= 33)
                {
                    stream.Write(item); // full item
                }
                else
                {
                    stream.Write(item.TemplateId); // compact item (0x96acc2 -> 0x968880)
                    stream.Write(item.Id);
                    stream.Write(item.Grade);
                }
            }
            else if (baseUnitType == BaseUnitType.Character)
            {
                // 10.8 Character equipment wire format (0x96ac99 -> 0x9686e0):
                WriteItemRecord108(stream, item);
            }
            else
            {
                stream.Write(item); // full item (Slave/Housing/Mate)
            }
        }

        if (baseUnitType == BaseUnitType.Character)
            stream.Write(0UL); // trailing per-slot flags (0x96adde).
    }

    private static void WriteItemRecord108(PacketStream stream, Item item)
    {
        // x2game.dll 0x9686e0
        stream.Write((uint)item.TemplateId); // templateId (u32, 0x968709)
        if (item.TemplateId == 0)
            return;

        stream.Write((ulong)item.Id);                     // id (u64, 0x96873b)
        stream.Write((byte)item.Grade);                   // grade (u8, 0x968754)
        stream.Write((byte)item.ItemFlags);               // flags (u8, 0x968770)
        stream.Write((int)(item.Count > 0 ? item.Count : 1)); // stackSize (i32, 0x96878c)

        // Item Details (0x96879b -> 0x968030)
        var detailType = (byte)item.DetailType;
        stream.Write(detailType); // detailType (u8, 0x968068)

        if (detailType == 1) // ItemDetailType.Equipment (0x968096)
        {
            stream.Write((byte)item.Durability);         // durability (u8, 0x9680c0)
            stream.Write((short)item.ChargeCount);       // chargeCount (i16, 0x9680db)
            stream.Write(item.ChargeTime);               // chargeTime (i64, 0x9680f6)
            stream.Write((ushort)item.TemperPhysical);   // temperPhysical (u16, 0x968111)
            stream.Write((ushort)item.EvolveChance);     // evolveChance (u16, 0x96812c)
            stream.Write(item.ChargeProcTime);           // chargeProcTime (i64, 0x968147)
            stream.Write((byte)item.MappingFailBonus);   // mappingFailBonus (u8, 0x968162)
            stream.Write((byte)item.ElementLevel);       // elementLevel (u8, 0x96817d)

            // Pisc gems (0x968289 - 0x968319: 18 values in chunks of 4, 4, 4, 4, 2)
            var gems = item.GemIds;
            stream.WritePisc(GetGem(gems, 0), GetGem(gems, 1), GetGem(gems, 2), GetGem(gems, 3));
            stream.WritePisc(GetGem(gems, 4), GetGem(gems, 5), GetGem(gems, 6), GetGem(gems, 7));
            stream.WritePisc(GetGem(gems, 8), GetGem(gems, 9), GetGem(gems, 10), GetGem(gems, 11));
            stream.WritePisc(GetGem(gems, 12), GetGem(gems, 13), GetGem(gems, 14), GetGem(gems, 15));
            stream.WritePisc(GetGem(gems, 16), GetGem(gems, 17));
        }
        else if (detailType is >= 2 and <= 14)
        {
            var len = GetDetailPayloadLength(detailType);
            var detail = item.Detail ?? Array.Empty<byte>();
            if (detail.Length == len)
            {
                stream.Write(detail);
            }
            else
            {
                var buf = new byte[len];
                if (detail.Length > 0)
                    Array.Copy(detail, buf, Math.Min(detail.Length, len));
                stream.Write(buf);
            }
        }

        // Tail (0x9686e0)
        stream.Write(item.CreateTime);                   // creationTime (i64, 0x9687bc)
        stream.Write((int)item.LifespanMins);            // lifespanMins (i32, 0x9687d9)
        stream.Write((ulong)item.MadeUnitId);            // madeUnitId (u64, 0x9687f6)
        stream.Write((byte)item.WorldId);                // worldId (u8, 0x968815)
        stream.Write(item.UnsecureTime);                 // unsecureDateTime (i64, 0x968834)
        stream.Write(item.UnpackTime);                   // unpackDateTime (i64, 0x968850)
        stream.Write(item.ChargeUseSkillTime);           // chargeUseSkillTime (i64, 0x96886c)
    }

    private static long GetGem(uint[] gems, int index)
    {
        return gems != null && index < gems.Length ? gems[index] : 0;
    }

    private static int GetDetailPayloadLength(byte detailType)
    {
        return detailType switch
        {
            2 => 33,
            3 => 20,
            4 => 9,
            5 => 24,
            6 => 16,
            7 => 16,
            8 => 8,
            9 => 4,
            10 => 12,
            11 => 24,
            12 => 10,
            13 => 13,
            14 => 8,
            _ => 0
        };
    }
}
