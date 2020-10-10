using Bindings;
using System;


namespace Server
{
    class GameLogic
    {
        private static ServerTCP stcp;

        public static void JoinGame(int index)
        {
            stcp = new ServerTCP();
            PacketBuffer buffer = new PacketBuffer();
            buffer.AddInteger((int)ServerPackets.SJoinGame);
            buffer.AddInteger(index - 1);
            buffer.AddString(Types.Player[index].Name);
            buffer.AddInteger(Types.Player[index].Map);

            stcp.SendData(index, buffer.ToArray());
            buffer.Dispose();
        }

    }
}
