using BattleShip;
using BattleShip.NetCore;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NetworkEngine_5._0.Server
{
    public static class ServerReader
    {

        public static void ReadTCPPacket(string packet, int sender)
        {

            NetPlay.PacketType packetID = (NetPlay.PacketType)GetPacketID(packet);

            switch (packetID)
            {

                case NetPlay.PacketType.allShipsArePlaced:

                    Handler.isEnemyReady = true;

                    while (!Play.Instance.IsAllShipArePlaced())
                    {
                        Thread.Sleep(1000);
                    }

                    Handler.playerTurn = 2;
                    ServerSender.SendReadyForBattle();

                    Play.Instance.SetTextLCD("attention . . .");

                    Thread.Sleep(4000);

                    ServerSender.SendChooseTarget();

                    Play.Instance.SetTextLCD("attente . . .");

                    break;


                case NetPlay.PacketType.target:

                    string data = GetDataTCP(packet);

                    int targetX = int.Parse(data.Split(":")[0]);
                    int targetY = int.Parse(data.Split(":")[1]);

                    Play.Instance.SetTextLCD("attaque en " + (targetX + 1) + "" + (char)('a' + (targetY)));

                    Thread.Sleep(3000);

                    Play.AttackResult result = Play.Instance.Attack(targetX, targetY);

                    ServerSender.SendAttackResult((int)result, targetX, targetY);

                    Play.Instance.SetTextLCD("choisir une cible");
                    Handler.playerTurn = 1;

                    break;

                case NetPlay.PacketType.attackResult:

                    data = GetDataTCP(packet);

                    Play.AttackResult result2 = (Play.AttackResult)int.Parse(data.Split(":")[0]);
                    int x = int.Parse(data.Split(":")[1]);
                    int y = int.Parse(data.Split(":")[2]);

                    if (result2 == Play.AttackResult.Missed)
                    {
                        Play.Instance.SetTextLCD("cible rate");
                        Play.Instance.attackCase[x, y] = 1;
                    }
                    else if (result2 == Play.AttackResult.Hited)
                    {
                        Play.Instance.SetTextLCD("cible touche");
                        Play.Instance.attackCase[x, y] = 2;
                    }
                    else if (result2 == Play.AttackResult.Sunk)
                    {
                        Play.Instance.SetTextLCD("cible coule");
                        Play.Instance.attackCase[x, y] = 2;
                    }

                    break;

                //case NetPlay.PacketType.distroyedObject:

                //    int index = int.Parse(GetDataTCP(packet));

                //    if (index < Handler.actors.Count && index >= 0)
                //    {
                //        if (Handler.actors[index].actorType == Actor.ActorType.Item)
                //            Main.Money += (int)((ItemV2)Handler.actors[index]).ID;
                //        if (Handler.actors[index].actorType == Actor.ActorType.Object)
                //            if (((ObjectV2)Handler.actors[index]).objectID == ObjectV2.ObjectID.coin)
                //                Main.Money += 1;

                //        ServerSender.SendDistroyedObject(index, sender);

                //        LightManager.lights.Remove(Handler.actors[index].light);
                //        Handler.actors.RemoveAt(index);
                //    }

                //    break;




            }

        }

        public static int GetPacketID(string data)
        {
            return int.Parse(data.Substring(1, 4));
        }

        public static string GetDataUDP(string packet)
        {
            return packet.Substring(7, packet.Length - 7);
        }

        public static string GetDataTCP(string packet)
        {
            return packet.Substring(5, packet.Length - 5);
        }

        public static string GetSenderID(string packet)
        {
            return packet.Substring(6, 1);
        }



        public static void ReadUDPPacket(string packet)
        {

            NetPlay.PacketType packetID = (NetPlay.PacketType)GetPacketID(packet);
            int playerID = int.Parse(GetSenderID(packet));
            string[] data = GetDataUDP(packet).Split(";");

            switch (packetID)
            {

                //case NetPlay.PacketType.playerPosition:

                //    int id = int.Parse(data[0]);
                //    float pX = float.Parse(data[1]);
                //    float pY = float.Parse(data[2]);
                //    bool isRight = bool.Parse(data[3]);

                //    Handler.playersV2[id].BasePosition = new Vector2(pX, pY);
                //    Handler.playersV2[id].isRight = isRight;

                //    ServerSender.SendPositionPlayer(playerID, Handler.playersV2[id].BasePosition.X, Handler.playersV2[id].BasePosition.Y, Handler.playersV2[id].isRight);

                //    break;



            }
        }

    }
}
