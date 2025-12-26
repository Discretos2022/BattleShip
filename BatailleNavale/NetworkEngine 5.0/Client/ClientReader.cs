using Microsoft.Xna.Framework;
using BattleShip;
using BattleShip.NetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Threading;

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

                case NetPlay.PacketType.readyForBattle:

                    Play.Instance.SetTextLCD("attention . . .");

                    break;

                case NetPlay.PacketType.chooseTarget:

                    Play.Instance.SetTextLCD("choisir une cible");
                    Handler.playerTurn = 2;

                    break;

                case NetPlay.PacketType.attackResult:

                    string data = GetData(packet);

                    Play.AttackResult result = (Play.AttackResult)int.Parse(data.Split(":")[0]);
                    int x = int.Parse(data.Split(":")[1]);
                    int y = int.Parse(data.Split(":")[2]);

                    if (result == Play.AttackResult.Missed)
                    {
                        Play.Instance.SetTextLCD("cible rate");
                        Play.Instance.attackCase[x, y] = 1;
                    }
                    else if (result == Play.AttackResult.Hited)
                    {
                        Play.Instance.SetTextLCD("cible touche");
                        Play.Instance.attackCase[x, y] = 2;
                    }
                    else if (result == Play.AttackResult.Sunk)
                    {
                        Play.Instance.SetTextLCD("cible coule");
                        Play.Instance.attackCase[x, y] = 2;
                    }

                    break;

                case NetPlay.PacketType.target:

                    data = GetData(packet);

                    int targetX = int.Parse(data.Split(":")[0]);
                    int targetY = int.Parse(data.Split(":")[1]);

                    Play.Instance.SetTextLCD("attaque en " + (targetX + 1) + "" + (char)('a' + (targetY)));

                    Thread.Sleep(3000);

                    Play.AttackResult result2 = Play.Instance.Attack(targetX, targetY);

                    ClientSender.SendAttackResult((int)result2, targetX, targetY);

                    Play.Instance.SetTextLCD("choisir une cible");
                    Handler.playerTurn = 2;

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
