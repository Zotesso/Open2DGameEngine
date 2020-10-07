using System;
using System.Collections.Generic;
using Bindings;

namespace Client
{
    class ClientHandleData
    {
        public PacketBuffer Buffer = new PacketBuffer();
        private delegate void Packet(byte[] data);
        private Dictionary<int, Packet> Packets;

        public void InitializeMessages()
        {
            Packets = new Dictionary<int, Packet>();
        }

        public void HandleNetworkMessages(byte[] data)
        {
            int packetNum;
            PacketBuffer buffer;
            buffer = new PacketBuffer();

            buffer.AddByteArray(data);
            packetNum = buffer.GetInteger();
            buffer.Dispose();

            if(Packets.TryGetValue(packetNum, out Packet Packet))
            {
                Packet.Invoke(data);
            }
        }
    }
}
