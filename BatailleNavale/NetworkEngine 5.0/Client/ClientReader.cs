using Microsoft.Xna.Framework;
using BattleShip;
using BattleShip.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace NetworkEngine_5._0.Client
{
    public static class ClientReader
    {

        public static void ReadTCPPacket(string packet)
        {
            NetPlay.PacketType packetID = (NetPlay.PacketType)GetPacketID(packet);

            switch (packetID)
            {

                case NetPlay.PacketType.startGame:

                    Main.gameState = GameState.Playing;

                    break;

                //case NetPlay.PacketType.otherPlayerJoined:
                //    //if(!Handler.playersV2.ContainsKey(int.Parse(GetData(packet)) + 1))
                //        //Handler.AddPlayerV2(int.Parse(GetData(packet)) + 1, NetPlay.MyPlayerID());

                //    int id = int.Parse(GetData(packet));

                //    if (id == 1)
                //        NetPlay.usedPlayerID.Clear();

                //    if (!NetPlay.usedPlayerID.Contains(id))
                //        NetPlay.usedPlayerID.Add(id);

                //    NetPlay.usedPlayerID.OrderBy(n => n);

                //    break;

            }

        }

        public static int GetPacketID(string packet)
        {
            return int.Parse(packet.Substring(1, 4));
        }

        public static string GetData(string packet)
        {
            return packet.Substring(5, packet.Length - 5);
        }





        public static void ReadUDPPacket(string packet)
        {
            NetPlay.PacketType packetID = (NetPlay.PacketType)GetPacketID(packet);

            switch (packetID)
            {
                //case NetPlay.PacketType.playerOneWorldMapPosition:

                //    string data = GetData(packet);

                //    int x = int.Parse(data.Split(';')[0]);
                //    int y = int.Parse(data.Split(';')[1]);

                //    WorldMap.SetLevelSelectorPos(new Vector2(x, y));

                //    break;


            }
        }

    }
}
