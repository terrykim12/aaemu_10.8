using System;

using AAEmu.Commons.Network;

using Xunit;

namespace AAEmu.UnitTests.Commons.Utils;

public class PositionCodecTests
{
    [Theory]
    [InlineData(0f, 0f, 0f)]
    [InlineData(10322.5f, 16014.4f, 356.6f)]
    [InlineData(-512.25f, 2048.75f, -25.5f)]
    public void PositionCodecUsesElevenBytesAndRoundTrips(float x, float y, float z)
    {
        var stream = new PacketStream();
        stream.WritePosition(x, y, z);

        Assert.Equal(11, stream.Count);

        stream.Rollback();
        var actual = stream.ReadPosition();
        Assert.InRange(Math.Abs(actual.x - x), 0f, 0.01f);
        Assert.InRange(Math.Abs(actual.y - y), 0f, 0.01f);
        Assert.InRange(Math.Abs(actual.z - z), 0f, 0.01f);
        Assert.False(stream.HasBytes);
    }
}
