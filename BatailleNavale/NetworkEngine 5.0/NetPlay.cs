using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NetworkEngine_5._0.Client;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace BattleShip.NetCore
{


    public static class NetPlay
    {
        public static bool IsMultiplaying = false;

        public static string LocalIPAddress()
        {
            string result = "";
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress[] addressList = hostEntry.AddressList;
            for (int i = 0; i < addressList.Length; i++)
            {
                IPAddress iPAddress = addressList[i];
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    result = iPAddress.ToString();
                    break;
                }
            }
            return result;
        }

        public enum PacketType
        {
            None = 0,
            playerID = 1,
            startGame = 2,
        }

        public static int MyPlayerID()
        {
            if (NetworkEngine_5._0.Client.Client.GetState() == Client.ClientState.Connected)
                return NetworkEngine_5._0.Client.Client.ID + 1;

            return 1;
        }

        public static List<int> usedPlayerID = new List<int>();

    }

}
