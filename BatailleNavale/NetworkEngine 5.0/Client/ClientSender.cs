using BattleShip.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkEngine_5._0.Client
{
    public class ClientSender
    {

        //public static void SendDistroyedObject(int index)
        //{
        //    string packet = CreateTCPpacket(index.ToString(), NetPlay.PacketType.distroyedObject);
        //    Client.SendTCP(packet);
        //}

        public static void SendAllShipsArePlaced()
        {
            string packet = CreateTCPpacket("", NetPlay.PacketType.allShipsArePlaced);
            Client.SendTCP(packet);
        }




        //public static void SendPositionPlayer(int PlayerID, float x, float y, bool isRight)
        //{
        //    string packet = CreateUDPpacket(PlayerID + ";" + x + ";" + y + ";" + isRight, NetPlay.PacketType.playerPosition);
        //    Client.SendUDP(packet);
        //}



        private static string CreateTCPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + data;
        }

        private static string CreateUDPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + NetPlay.MyPlayerID() + " " + data;
        }


    }
}
