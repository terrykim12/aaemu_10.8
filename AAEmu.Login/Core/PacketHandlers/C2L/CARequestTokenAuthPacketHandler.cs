using AAEmu.Login.Core.Authentication;
using AAEmu.Login.Core.Controllers;
using AAEmu.Login.Core.Network.Connections;
using AAEmu.Login.Core.Packets.C2L;

namespace AAEmu.Login.Core.PacketHandlers.C2L;

/// <summary>
/// Handles the 10.8 token auth packet (0x17).
/// </summary>
public class CARequestTokenAuthPacketHandler(ILoginController loginController)
    : ILoginPacketHandler<CARequestTokenAuthPacket>
{
    public async Task Execute(CARequestTokenAuthPacket packet, ILoginSession session,
        CancellationToken cancellationToken)
    {
        var account = packet.Token;
        if (string.IsNullOrEmpty(account))
            account = "admin";

        var flow = new TokenAuthFlow(loginController, account, session.Connection.Ip);
        await session.AuthenticateAsync(flow, cancellationToken);
    }
}