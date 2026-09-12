using System;
using AAEmu.Commons.Network;
using AAEmu.Game.Models.Game.Items;

namespace AAEmu.Game.Models.Game.Units;

public enum UnitCustomModelType : byte
{
    None = 0,
    Hair = 1,
    Skin = 2,
    Face = 3
}

// One fixed-decal slot (asset id + blend weight). In 10.0.2.13 the ids and weights are serialized in
// separate runs (see FaceModel), so this is a plain data holder.
public class FixedDecalAsset(uint assetId = 0, float assetWeight = 0)
{
    public uint AssetId { get; set; } = assetId;
    public float AssetWeight { get; set; } = assetWeight;
}

// 10.0.2.13 face customization tier (LobbyChar_WriteAppearance T3 block).
// Wire order: movable face-decal transform, then a pish/pisc group of the 6
// fixed-decal asset ids, then a pish/pisc group of {diffuse, normal, eyelash} map ids, then the 6 fixed-decal
// weights, then the normal-map weight, then the 5 colors, then the 128-byte face-maker blob.
public class FaceModel : PacketMarshaler
{
    public uint MovableDecalAssetId { get; set; }
    public float MovableDecalWeight { get; set; }
    public float MovableDecalScale { get; set; }
    public float MovableDecalRotate { get; set; }
    public short MovableDecalMoveX { get; set; }
    public short MovableDecalMoveY { get; set; }

    public FixedDecalAsset[] FixedDecalAsset { get; }

    public uint DiffuseMapId { get; set; }
    public uint NormalMapId { get; set; }
    public uint EyelashMapId { get; set; }
    public float NormalMapWeight { get; set; }
    public uint LipColor { get; set; }
    public uint LeftPupilColor { get; set; }
    public uint RightPupilColor { get; set; }
    public uint EyebrowColor { get; set; }
    public uint DecoColor { get; set; }

    public byte[] Modifier { get; set; }

    public FaceModel()
    {
        FixedDecalAsset = new FixedDecalAsset[6]; // 10.0.2.13 widened the fixed-decal count from 4 to 6
        for (var i = 0; i < FixedDecalAsset.Length; i++)
            FixedDecalAsset[i] = new FixedDecalAsset();

        Modifier = new byte[128];
    }

    public bool SetFixedDecalAsset(byte index, uint id, float weight)
    {
        if (FixedDecalAsset.Length <= index)
            return false;

        FixedDecalAsset[index].AssetId = id;
        FixedDecalAsset[index].AssetWeight = weight;

        return true;
    }

    public override void Read(PacketStream stream)
    {
        MovableDecalAssetId = stream.ReadUInt32();
        MovableDecalWeight = stream.ReadSingle();
        MovableDecalScale = stream.ReadSingle();
        MovableDecalRotate = stream.ReadSingle();
        MovableDecalMoveX = stream.ReadInt16();
        MovableDecalMoveY = stream.ReadInt16();

        var decalIds1 = stream.ReadPisc(4);
        var decalIds2 = stream.ReadPisc(2);
        var mapIds = stream.ReadPisc(3);
        DiffuseMapId = (uint)mapIds[0];
        NormalMapId = (uint)mapIds[1];
        EyelashMapId = (uint)mapIds[2];

        FixedDecalAsset[0].AssetId = (uint)decalIds1[0];
        FixedDecalAsset[1].AssetId = (uint)decalIds1[1];
        FixedDecalAsset[2].AssetId = (uint)decalIds1[2];
        FixedDecalAsset[3].AssetId = (uint)decalIds1[3];
        FixedDecalAsset[4].AssetId = (uint)decalIds2[0];
        FixedDecalAsset[5].AssetId = (uint)decalIds2[1];

        for (var i = 0; i < 6; i++)
            FixedDecalAsset[i].AssetWeight = stream.ReadSingle();

        NormalMapWeight = stream.ReadSingle();
        LipColor = stream.ReadUInt32();
        LeftPupilColor = stream.ReadUInt32();
        RightPupilColor = stream.ReadUInt32();
        EyebrowColor = stream.ReadUInt32();
        DecoColor = stream.ReadUInt32();

        Modifier = stream.ReadBytes(); // 10.8 face-maker morph sliders (u16 length + 128 bytes)
    }

    public override PacketStream Write(PacketStream stream)
    {
        // movable face-decal transform
        stream.Write(MovableDecalAssetId);
        stream.Write(MovableDecalWeight);
        stream.Write(MovableDecalScale);
        stream.Write(MovableDecalRotate);
        stream.Write(MovableDecalMoveX);
        stream.Write(MovableDecalMoveY);

        stream.WritePisc(FixedDecalAsset[0].AssetId, FixedDecalAsset[1].AssetId, FixedDecalAsset[2].AssetId, FixedDecalAsset[3].AssetId);
        stream.WritePisc(FixedDecalAsset[4].AssetId, FixedDecalAsset[5].AssetId);
        stream.WritePisc(DiffuseMapId, NormalMapId, EyelashMapId);

        // 6 fixed-decal weights, then the normal-map weight
        for (var i = 0; i < 6; i++)
            stream.Write(FixedDecalAsset[i].AssetWeight);
        stream.Write(NormalMapWeight);

        stream.Write(LipColor);
        stream.Write(LeftPupilColor);
        stream.Write(RightPupilColor);
        stream.Write(EyebrowColor);
        stream.Write(DecoColor);

        stream.Write(Modifier, true); // 10.8 face-maker morph sliders (u16 length + 128 bytes)
        return stream;
    }
}

// 10.8 appearance block (LobbyChar_WriteAppearance). A leading `ext` byte
// (UnitCustomModelType) is a cumulative LOD gate: 0 = nothing, 1 = +hair (T1), 2 = +body (T2), >=3 = +face (T3).
// Shared by SC_PACKET_UNIT_STATE, the character list, and the CSCreateCharacter body.
public class UnitCustomModelParams : PacketMarshaler
{
    private UnitCustomModelType _type;

    public uint Id { get; set; }

    // T1 — base appearance
    public uint HairColorId { get; set; }        // defaultHairColor / hair style

    // T2
    public uint HairColor { get; set; }
    public uint HornColor { get; set; }
    public uint HornColorId { get => HornColor; set => HornColor = value; }

    // T3
    public uint DefaultHairColor { get; set; }
    public uint TwoToneHairColor { get; set; }
    public uint TwoToneHair { get => TwoToneHairColor; set => TwoToneHairColor = value; }
    public float TwoToneFirstWidth { get; set; }
    public float TwoToneSecondWidth { get; set; }
    public uint SkinColorId { get; set; }        // wire `skinColor`
    public uint BodyDiffuseMap { get; set; }
    public uint BodyNormalMap { get; set; }
    public uint BodyNormalMapId { get => BodyNormalMap; set => BodyNormalMap = value; }
    public float BodyNormalMapWeight { get; set; }
    public float BodyWeight { get; set; }

    // Kept for callers; the unit's model id is sent separately (unit-state modelRef), not in this block.
    public uint ModelId { get; set; }

    // Face (ext >= Face)
    public FaceModel Face { get; private set; }

    // Base identity fields placed after Face in 10.8
    public byte Race { get; set; }
    public byte Gender { get; set; }
    public long VisualRaceExpiredTime { get; set; }
    public byte VisualRace { get; set; }
    public byte VisualGender { get; set; }

    // 10.8 Wings customization
    public uint WingColor { get; set; }
    public byte WingScale { get; set; } = 100;
    public sbyte WingOffsetX { get; set; }
    public sbyte WingOffsetY { get; set; }
    public sbyte WingOffsetZ { get; set; }

    public UnitCustomModelParams(UnitCustomModelType type = UnitCustomModelType.None)
    {
        SetType(type);
    }

    public UnitCustomModelParams SetId(uint id)
    {
        Id = id;
        return this;
    }

    public UnitCustomModelParams SetType(UnitCustomModelType type)
    {
        _type = type;
        if (_type >= UnitCustomModelType.Face && Face == null)
            Face = new FaceModel();
        return this;
    }

    public UnitCustomModelParams SetModelId(uint modelId)
    {
        ModelId = modelId;
        return this;
    }

    public UnitCustomModelParams SetBodyNormalMapId(uint bodyNormalMapId)
    {
        BodyNormalMapId = bodyNormalMapId;
        return this;
    }

    public UnitCustomModelParams SetBodyNormalMapWeight(float weight)
    {
        BodyNormalMapWeight = weight;
        return this;
    }

    public UnitCustomModelParams SetDefaultHairColor(uint defaultHairColor)
    {
        DefaultHairColor = defaultHairColor;
        return this;
    }

    public UnitCustomModelParams SetHairColorId(uint hairColorId)
    {
        HairColorId = hairColorId;
        return this;
    }

    public UnitCustomModelParams SetHornColorId(uint hornColorId)
    {
        HornColorId = hornColorId;
        return this;
    }

    public UnitCustomModelParams SetSkinColorId(uint skinColorId)
    {
        SkinColorId = skinColorId;
        return this;
    }

    public UnitCustomModelParams SetTwoToneFirstWidth(float twoToneFirstWidth)
    {
        TwoToneFirstWidth = twoToneFirstWidth;
        return this;
    }

    public UnitCustomModelParams SetTwoToneHair(uint twoToneHair)
    {
        TwoToneHair = twoToneHair;
        return this;
    }

    public UnitCustomModelParams SetTwoToneSecondWidth(float twoToneSecondWidth)
    {
        TwoToneSecondWidth = twoToneSecondWidth;
        return this;
    }

    public UnitCustomModelParams SetFace(FaceModel face)
    {
        Face = face;
        return this;
    }

    public override void Read(PacketStream stream)
    {
        SetType((UnitCustomModelType)stream.ReadByte()); // ext

        if (_type < UnitCustomModelType.Hair)
            return;

        // T1
        HairColorId = stream.ReadUInt32();

        if (_type < UnitCustomModelType.Skin)
            return;

        // T2
        HairColor = stream.ReadUInt32();
        HornColor = stream.ReadUInt32();

        if (_type < UnitCustomModelType.Face)
            return;

        // T3
        HairColorId = stream.ReadUInt32();
        DefaultHairColor = stream.ReadUInt32();
        TwoToneHairColor = stream.ReadUInt32();
        TwoToneFirstWidth = stream.ReadSingle();
        TwoToneSecondWidth = stream.ReadSingle();
        SkinColorId = stream.ReadUInt32();
        BodyWeight = stream.ReadSingle();

        if (Face == null)
            Face = new FaceModel();
        Face.Read(stream);

        Race = stream.ReadByte();
        Gender = stream.ReadByte();
        VisualRaceExpiredTime = stream.ReadInt64();
        VisualRace = stream.ReadByte();
        VisualGender = stream.ReadByte();

        WingColor = stream.ReadUInt32();
        WingScale = stream.ReadByte();
        WingOffsetX = (sbyte)stream.ReadByte();
        WingOffsetY = (sbyte)stream.ReadByte();
        WingOffsetZ = (sbyte)stream.ReadByte();
    }

    internal static UnitCustomModelParams ReadStored(byte[] data)
    {
        // Packed asset IDs make the current appearance record variable-length.
        var stream = new PacketStream(data);
        var model = new UnitCustomModelParams();
        model.Read(stream);
        if (stream.LeftBytes != 0)
            throw new FormatException($"Appearance record contains {stream.LeftBytes} unread bytes.");
        return model;
    }

    public void ReadLegacy266(PacketStream stream)
    {
        SetType((UnitCustomModelType)stream.ReadByte()); // ext (1B)

        // 13 uint/float fields (52B)
        HairColorId = stream.ReadUInt32();
        var f1 = stream.ReadUInt32();
        var f2 = stream.ReadUInt32();
        var f3 = stream.ReadUInt32();
        var f4 = stream.ReadUInt32();
        HairColor = stream.ReadUInt32();
        TwoToneHairColor = stream.ReadUInt32();
        TwoToneFirstWidth = stream.ReadSingle();
        TwoToneSecondWidth = stream.ReadSingle();
        SkinColorId = stream.ReadUInt32();
        BodyDiffuseMap = stream.ReadUInt32();
        BodyNormalMap = stream.ReadUInt32();
        BodyWeight = stream.ReadSingle();

        if (Face == null)
            Face = new FaceModel();
        Face.Read(stream); // 213B

        // 10.8 Wings customization defaults
        WingColor = 0;
        WingScale = 100;
        WingOffsetX = 0;
        WingOffsetY = 0;
        WingOffsetZ = 0;
    }

    public override PacketStream Write(PacketStream stream)
    {
        stream.Write((byte)_type); // ext

        if (_type < UnitCustomModelType.Hair)
            return stream;

        // T1
        stream.Write(HairColorId);

        if (_type < UnitCustomModelType.Skin)
            return stream;

        // T2
        stream.Write(HairColor);
        stream.Write(HornColor);

        if (_type < UnitCustomModelType.Face)
            return stream;

        // T3
        stream.Write(HairColorId);
        stream.Write(DefaultHairColor);
        stream.Write(TwoToneHairColor);
        stream.Write(TwoToneFirstWidth);
        stream.Write(TwoToneSecondWidth);
        stream.Write(SkinColorId);
        stream.Write(BodyWeight);

        if (Face == null)
            Face = new FaceModel();
        stream.Write(Face);

        stream.Write(Race);
        stream.Write(Gender);
        stream.Write(VisualRaceExpiredTime);
        stream.Write(VisualRace);
        stream.Write(VisualGender);

        stream.Write(WingColor);
        stream.Write(WingScale == 0 ? (byte)100 : WingScale);
        stream.Write((byte)WingOffsetX);
        stream.Write((byte)WingOffsetY);
        stream.Write((byte)WingOffsetZ);

        return stream;
    }
}
