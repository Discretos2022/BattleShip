using BattleShip;
using BattleShip.NetCore;
using System.IO;
using System.Linq;

namespace NetworkEngine_5._0.Server
{
    public class ServerSender
    {

        public static void SendID(int ID, int clientID)
        {

            string packet = CreateTCPpacket(ID.ToString(), NetPlay.PacketType.playerID);
            Server.SendTCP(packet, clientID);
            //Handler.AddPlayerV2(ID, 1);

        }

        public static void SendStartGame()
        {

            string packet = CreateTCPpacket("", NetPlay.PacketType.startGame);
            Server.SendTCP(packet);

        }

        //public static void SendOtherPlayerID(int otherID)
        //{

        //    if (otherID == 1)
        //        NetPlay.usedPlayerID.Clear();

        //    string packet = CreateTCPpacket(otherID.ToString(), NetPlay.PacketType.otherPlayerJoined);
        //    Server.SendTCP(packet);
        //    //Handler.AddPlayerV2(otherID, 1);
        //    if(!NetPlay.usedPlayerID.Contains(otherID))
        //        NetPlay.usedPlayerID.Add(otherID);

        //    NetPlay.usedPlayerID.OrderBy(n => n);

        //}


        //public static void SendWorldMapPositionPlayer(int x, int y)
        //{
        //    string packet = CreateUDPpacket(x + ";" + y, NetPlay.PacketType.playerOneWorldMapPosition);
        //    Server.SendUDP(packet);
        //}


        private static string CreateTCPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + data;
        }

        private static string CreateUDPpacket(string data, NetPlay.PacketType type)
        {
            return "$" + ((int)type).ToString("0000") + " " + data;
        }


    }
}
