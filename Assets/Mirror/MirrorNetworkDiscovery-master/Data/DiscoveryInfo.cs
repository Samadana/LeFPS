﻿using Assets.Scripts.NetworkMessages;
using System.Net;

namespace Mirror
{
    public class DiscoveryInfo
    {
        public DiscoveryInfo(IPEndPoint endPoint, byte[] packetData)
        {
            this.EndPoint = endPoint;
            this.packetData = packetData;
        }
        public IPEndPoint EndPoint { get; }
        public byte[] packetData;

        public GameBroadcastPacket unpackedData = null;
    }
}