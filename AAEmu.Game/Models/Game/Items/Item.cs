using System;

using AAEmu.Commons.Network;
using AAEmu.Game.Models.Game.Items.Containers;
using AAEmu.Game.Models.Game.Items.Templates;

namespace AAEmu.Game.Models.Game.Items;

public class Item : PacketMarshaler, IComparable<Item>
{
    private byte _worldId;
    private ulong _ownerId;
    private ulong _id;
    private uint _templateId;
    private SlotType _slotType;
    private int _slot;
    private byte _grade;
    private ItemFlag _itemFlags;
    private int _count;
    private int _lifespanMins;
    private uint _madeUnitId;
    private DateTime _createTime;
    private DateTime _unsecureTime;
    private DateTime _unpackTime;
    private uint _imageItemTemplateId;
    private bool _isDirty;
    private ulong _uccId;
    private DateTime _expirationTime;
    private double _expirationOnlineMinutesLeft;
    private DateTime _chargeUseSkillTime;
    private byte _flags;
    private byte _durability;
    private short _chargeCount;
    private ushort _TemperPhysical;
    private ushort _TemperMagical;
    private uint _runeId;
    private DateTime _chargeTime;

    public bool IsDirty { get => _isDirty; set => _isDirty = value; }
    public byte WorldId { get => _worldId; set { _worldId = value; _isDirty = true; } }
    public ulong OwnerId { get => _ownerId; set { _ownerId = value; _isDirty = true; } }
    public ulong Id { get => _id; set { _id = value; _isDirty = true; } }
    public uint TemplateId { get => _templateId; set { _templateId = value; _isDirty = true; } }
    public ItemTemplate Template { get; set; }
    public virtual uint DetailBytesLength { get; } = 0;
    public SlotType SlotType { get => _slotType; set { _slotType = value; _isDirty = true; } }
    public int Slot { get => _slot; set { _slot = value; _isDirty = true; } }
    public byte Grade { get => _grade; set { _grade = value; _isDirty = true; } }
    public ItemFlag ItemFlags { get => _itemFlags; set { _itemFlags = value; _isDirty = true; } }
    public int Count { get => _count; set { _count = value; _isDirty = true; } }
    public int LifespanMins { get => _lifespanMins; set { _lifespanMins = value; _isDirty = true; } }
    public uint MadeUnitId { get => _madeUnitId; set { _madeUnitId = value; _isDirty = true; } }
    public DateTime CreateTime { get => _createTime; set { _createTime = value; _isDirty = true; } }
    public DateTime UnsecureTime { get => _unsecureTime; set { _unsecureTime = value; _isDirty = true; } }
    public DateTime UnpackTime { get => _unpackTime; set { _unpackTime = value; _isDirty = true; } }
    public uint ImageItemTemplateId { get => _imageItemTemplateId; set { _imageItemTemplateId = value; _isDirty = true; } }

    /// <summary>
    /// Internal representation of the exact time a item will expire (UTC)
    /// </summary>
    public DateTime ExpirationTime
    {
        get => _expirationTime;
        set
        {
            if (_expirationTime != value)
            {
                _expirationTime = value;
                _isDirty = true;
            }
        }
    }

    /// <summary>
    /// Internal representation of the time this item has left before expiring, only counting down if the owning character is online
    /// </summary>
    public double ExpirationOnlineMinutesLeft
    {
        get => _expirationOnlineMinutesLeft;
        set
        {
            _expirationOnlineMinutesLeft = value;
            _isDirty = true;
        }
    }

    public ulong UccId
    {
        get => _uccId;
        set
        {
            _uccId = value;
            if (value > 0)
                SetFlag(ItemFlag.HasUCC);
            else
                RemoveFlag(ItemFlag.HasUCC);
            _isDirty = true;
        }
    }

    public DateTime ChargeStartTime { get; set; } = DateTime.MinValue;
    public virtual ItemDetailType DetailType { get; set; } // TODO 1.0 max type: 8, at 1.2 max type 9, at 3.0.3.0 max type 10, at 3.5.0.3 max type 12
    public DateTime ChargeUseSkillTime { get => _chargeUseSkillTime; set { _chargeUseSkillTime = value; _isDirty = true; } }
    public byte Flags { get => _flags; set { _flags = value; _isDirty = true; } }
    public byte Durability { get => _durability; set { _durability = value; _isDirty = true; } }
    public short ChargeCount { get => _chargeCount; set { _chargeCount = value; _isDirty = true; } }
    public DateTime ChargeTime { get => _chargeTime; set { _chargeTime = value; _isDirty = true; } }
    public ushort TemperPhysical { get => _TemperPhysical; set { _TemperPhysical = value; _isDirty = true; } }
    public ushort TemperMagical { get => _TemperMagical; set { _TemperMagical = value; _isDirty = true; } }
    public ushort EvolveChance { get; set; }
    public DateTime ChargeProcTime { get; set; }
    public byte MappingFailBonus { get; set; }
    public byte ElementLevel { get; set; }
    public uint RuneId { get => _runeId; set { _runeId = value; _isDirty = true; } }

    public uint[] GemIds { get; set; }
    public byte[] Detail { get; set; }

    // Helper
    public ItemContainer _holdingContainer { get; set; }

    public static uint Coins { get; } = 500;
    public static uint TaxCertificate { get; } = 31891;
    public static uint BoundTaxCertificate { get; } = 31892;
    public static uint AppraisalCertificate { get; } = 28085;
    public static uint CrestStamp { get; } = 17662;
    public static uint CrestInk { get; } = 17663;
    public static uint SheetMusic { get; } = 28051;
    public static uint SalonCertificate { get; } = 30811;

    /// <summary>
    /// Sort will use itemSlot numbers
    /// </summary>
    /// <param name="otherItem"></param>
    /// <returns></returns>
    public int CompareTo(Item otherItem)
    {
        if (otherItem == null) return 1;
        return this.Slot.CompareTo(otherItem.Slot);
    }

    public Item()
    {
        WorldId = AppConfiguration.Instance.Id;
        OwnerId = 0;
        Slot = -1;
        _holdingContainer = null;
        _isDirty = true;
        GemIds = new uint[16];
    }

    public Item(byte worldId)
    {
        WorldId = worldId;
        OwnerId = 0;
        Slot = -1;
        _holdingContainer = null;
        _isDirty = true;
        GemIds = new uint[16];
    }

    public Item(ulong id, ItemTemplate template, int count)
    {
        WorldId = AppConfiguration.Instance.Id;
        OwnerId = 0;
        Id = id;
        TemplateId = template.Id;
        Template = template;
        Count = count;
        Slot = -1;
        _holdingContainer = null;
        _isDirty = true;
        GemIds = new uint[16];
    }

    public Item(byte worldId, ulong id, ItemTemplate template, int count)
    {
        WorldId = worldId;
        OwnerId = 0;
        Id = id;
        TemplateId = template.Id;
        Template = template;
        Count = count;
        Slot = -1;
        _holdingContainer = null;
        _isDirty = true;
        GemIds = new uint[18];
    }

    public override void Read(PacketStream stream)
    {
        TemplateId = stream.ReadUInt32();
        if (TemplateId == 0)
            return;

        Id = stream.ReadUInt64();
        Grade = stream.ReadByte();
        ItemFlags = (ItemFlag)stream.ReadByte();
        Count = stream.ReadInt32();

        DetailType = (ItemDetailType)stream.ReadByte();
        ReadDetails(stream);

        CreateTime = stream.ReadDateTime();
        LifespanMins = stream.ReadInt32();
        MadeUnitId = (uint)stream.ReadUInt64(); // 10.8 reads u64
        WorldId = stream.ReadByte();
        UnsecureTime = stream.ReadDateTime();
        UnpackTime = stream.ReadDateTime();
        ChargeUseSkillTime = stream.ReadDateTime();
    }

    public override PacketStream Write(PacketStream stream)
    {
        stream.Write((uint)TemplateId); // type
        if (TemplateId == 0)
            return stream;

        stream.Write((ulong)Id);    // id
        stream.Write((byte)Grade); // grade
        stream.Write((byte)ItemFlags); // flags | bounded
        stream.Write((int)(Count > 0 ? Count : 1)); // stackSize

        stream.Write((byte)DetailType); // detailType
        WriteDetails(stream);

        stream.Write(CreateTime);
        stream.Write((int)LifespanMins);
        stream.Write((ulong)MadeUnitId); // 10.8 writes u64
        stream.Write((byte)WorldId);
        stream.Write(UnsecureTime);
        stream.Write(UnpackTime);
        stream.Write(ChargeUseSkillTime);

        return stream;
    }

    public virtual void ReadDetails(PacketStream stream)
    {
        var detailType = (byte)DetailType;
        if (detailType == 1) // ItemDetailType.Equipment
        {
            if (stream.Count <= 35) // legacy 35-byte DB record
            {
                if (stream.LeftBytes < 15)
                    return;
                Durability = stream.ReadByte();       // durability
                ChargeCount = stream.ReadInt16();     // chargeCount
                ChargeTime = stream.ReadDateTime();   // chargeTime
                TemperPhysical = stream.ReadUInt16(); // scaledA
                TemperMagical = stream.ReadUInt16();  // scaledB
                if (GemIds == null || GemIds.Length < 18)
                    GemIds = new uint[18];
                for (int i = 0; i < 4; i++)
                {
                    var g = stream.ReadPisc(4);
                    for (int j = 0; j < 4; j++)
                        GemIds[i * 4 + j] = (uint)g[j];
                }
                return;
            }

            if (stream.LeftBytes < 25)
                return;
            Durability = stream.ReadByte();       // durability
            ChargeCount = stream.ReadInt16();     // chargeCount
            ChargeTime = stream.ReadDateTime();   // chargeTime
            TemperPhysical = stream.ReadUInt16(); // scaledA
            EvolveChance = stream.ReadUInt16();
            ChargeProcTime = stream.ReadDateTime();
            MappingFailBonus = stream.ReadByte();
            ElementLevel = stream.ReadByte();

            if (GemIds == null || GemIds.Length < 18)
                GemIds = new uint[18];

            var mGems = stream.ReadPisc(4);
            GemIds[0] = (uint)mGems[0];
            GemIds[1] = (uint)mGems[1];
            GemIds[2] = (uint)mGems[2];
            GemIds[3] = (uint)mGems[3];

            mGems = stream.ReadPisc(4);
            GemIds[4] = (uint)mGems[0];
            GemIds[5] = (uint)mGems[1];
            GemIds[6] = (uint)mGems[2];
            GemIds[7] = (uint)mGems[3];

            mGems = stream.ReadPisc(4);
            GemIds[8] = (uint)mGems[0];
            GemIds[9] = (uint)mGems[1];
            GemIds[10] = (uint)mGems[2];
            GemIds[11] = (uint)mGems[3];

            mGems = stream.ReadPisc(4);
            GemIds[12] = (uint)mGems[0];
            GemIds[13] = (uint)mGems[1];
            GemIds[14] = (uint)mGems[2];
            GemIds[15] = (uint)mGems[3];

            mGems = stream.ReadPisc(2);
            GemIds[16] = (uint)mGems[0];
            GemIds[17] = (uint)mGems[1];
        }
        else if (detailType is >= 2 and <= 14)
        {
            var len = GetDetailPayloadLength(detailType);
            if (len > 0 && stream.LeftBytes >= len)
                Detail = stream.ReadBytes(len);
        }
    }

    public virtual void WriteDetails(PacketStream stream)
    {
        var detailType = (byte)DetailType;
        if (detailType == 1) // ItemDetailType.Equipment
        {
            stream.Write((byte)Durability);         // durability
            stream.Write((short)ChargeCount);       // chargeCount
            stream.Write(ChargeTime);               // chargeTime
            stream.Write((ushort)TemperPhysical);   // scaledA
            stream.Write((ushort)EvolveChance);     // evolveChance
            stream.Write(ChargeProcTime);           // chargeProcTime
            stream.Write((byte)MappingFailBonus);   // mappingFailBonus
            stream.Write((byte)ElementLevel);       // elementLevel

            var gems = GemIds;
            long GetGem(int index) => gems != null && index < gems.Length ? gems[index] : 0;
            stream.WritePisc(GetGem(0), GetGem(1), GetGem(2), GetGem(3));
            stream.WritePisc(GetGem(4), GetGem(5), GetGem(6), GetGem(7));
            stream.WritePisc(GetGem(8), GetGem(9), GetGem(10), GetGem(11));
            stream.WritePisc(GetGem(12), GetGem(13), GetGem(14), GetGem(15));
            stream.WritePisc(GetGem(16), GetGem(17));
        }
        else if (detailType is >= 2 and <= 14)
        {
            var len = GetDetailPayloadLength(detailType);
            var detail = Detail ?? Array.Empty<byte>();
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
    }

    public static int GetDetailPayloadLength(byte detailType)
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

    public virtual bool HasFlag(ItemFlag flag)
    {
        return (ItemFlags & flag) == flag;
    }

    public virtual void SetFlag(ItemFlag flag)
    {
        ItemFlags |= flag;
    }

    public virtual void RemoveFlag(ItemFlag flag)
    {
        ItemFlags &= ~flag;
    }
}
