using AAEmu.Login.Core.Network.Internal;
using AAEmu.Login.Core.Network.Login;
using AAEmu.Login.Core.PacketHandlers.C2L;
using AAEmu.Login.Core.PacketHandlers.G2L;
using AAEmu.Login.Core.Packets.C2L;
using AAEmu.Login.Core.Packets.G2L;

namespace AAEmu.Login.Core.PacketHandlers;

public static class ServiceCollectionExtensions
{
    public static void AddInternalPacketHandlers(this IServiceCollection services)
    {
        services
            .AddInternalPacket<GLRegisterGameServerPacket, GLRegisterGameServerPacketHandler>()
            .AddInternalPacket<GLRequestInfoPacket, GLRequestInfoPacketHandler>()
            .AddInternalPacket<GLPlayerReconnectPacket, GLPlayerReconnectPacketHandler>()
            .AddInternalPacket<GLPlayerEnterPacket, GLPlayerEnterPacketHandler>()
            .AddInternalPacket<GLGameServerLoadPacket, GLGameServerLoadPacketHandler>();
    }

    public static void AddLoginPacketHandlers(this IServiceCollection services)
    {
        services
            .AddLoginPacket<CACancelEnterWorldPacket, CACancelEnterWorldPacketHandler>()
            .AddLoginPacket<CACancelEnterWorldPacket, CACancelEnterWorldPacketHandler>(0x00e)
            .AddLoginPacket<CAChallengeResponse2Packet, CAChallengeResponse2PacketHandler>()
            .AddLoginPacket<CAChallengeResponsePacket, CAChallengeResponsePacketHandler>()
            .AddLoginPacket<CAEnterWorldPacket, CAEnterWorldPacketHandler>()
            .AddLoginPacket<CAEnterWorldPacket, CAEnterWorldPacketHandler>(0x00d)
            .AddLoginPacket<CAListWorldPacket, CAListWorldPacketHandler>()
            .AddLoginPacket<CAListWorldPacket, CAListWorldPacketHandler>(0x00c)
            .AddLoginPacket<CAOtpNumberPacket, CAOtpNumberPacketHandler>()
            .AddLoginPacket<CAPcCertNumberPacket, CAPcCertNumberPacketHandler>()
            .AddLoginPacket<CAPongPacket, CAPongPacketHandler>()
            .AddLoginPacket<CAPongPacket, CAPongPacketHandler>(0x018)
            .AddLoginPacket<CARequestAuthPacket, CARequestAuthPacketHandler>()
            .AddLoginPacket<CARequestAuthPWDPacket, CARequestAuthPWDPacketHandler>()
            .AddLoginPacket<CARequestReconnectPacket, CARequestReconnectPacketHandler>()
            .AddLoginPacket<CARequestReconnectPacket, CARequestReconnectPacketHandler>(0x00f)
            .AddLoginPacket<CARequestVarifySNPacket, CARequestVarifySNPacketHandler>()
            .AddLoginPacket<CARequestWebAuthPacket, CARequestWebAuthPacketHandler>()
            .AddLoginPacket<CARequestTokenAuthPacket, CARequestTokenAuthPacketHandler>()
            .AddLoginPacket<CATestArsPacket, CATestArsPacketHandler>();
    }

    private static IServiceCollection AddLoginPacket<TPacket, TPacketHandler>(
        this IServiceCollection services, ushort? overrideTypeId = null)
        where TPacket : LoginPacket, ILoginPacket, new()
        where TPacketHandler : class, ILoginPacketHandler<TPacket>
    {
        services.AddSingleton<ILoginPacketHandler<TPacket>, TPacketHandler>();
        services.AddSingleton<ILoginPacketDescriptor>(sp =>
        {
            var handler = sp.GetServices<ILoginPacketHandler<TPacket>>().First();
            return new LoginPacketDescriptor<TPacket>(overrideTypeId ?? TPacket.TypeId, handler);
        });

        return services;
    }

    private static IServiceCollection AddInternalPacket<TPacket, TPacketHandler>(
        this IServiceCollection services)
        where TPacket : InternalPacket, IInternalPacket, new()
        where TPacketHandler : class, IInternalPacketHandler<TPacket>
    {
        services.AddSingleton<IInternalPacketHandler<TPacket>, TPacketHandler>();
        services.AddSingleton<IInternalPacketDescriptor>(sp =>
        {
            var handler = sp.GetRequiredService<IInternalPacketHandler<TPacket>>();
            return new InternalPacketDescriptor<TPacket>(TPacket.TypeId, handler);
        });

        return services;
    }
}
