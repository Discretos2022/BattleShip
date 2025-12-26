using BatailleNavale;
using BattleShip.NetCore;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NetworkEngine_5._0.Client;
using NetworkEngine_5._0.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRITER;

namespace BattleShip
{
    public class Play
    {

        private static readonly Lazy<Play> _instance = new(() => new Play());
        public static Play Instance => _instance.Value;

        public int[,] attackCase = new int[10, 10];
        private int[,] shipsCase = new int[10, 10];

        private Rectangle[,] cases = new Rectangle[10, 10];

        private string textQueue = "place tes navires";
        private int textTime = 0;
        private int maxTime = 5;
        private string textLCD = "";


        private Play()
        {

            for (int i = 0; i < cases.GetLength(0); i++)
            {
                for (int j = 0; j < cases.GetLength(1); j++)
                {
                    cases[i, j] = new Rectangle((int)GetPosition().X + (int)(16 * GetScale() + 1) + (i * (int)(16 * GetScale())), (int)GetPosition().Y + (int)(16 * GetScale() + 1) + (j * (int)(16 * GetScale())), (int)(16 * GetScale()), (int)(16 * GetScale()));
                }
            }

        }

        public void Update(GameTime gameTime, Screen screen)
        {

            Handler.Update(gameTime);

            if (Handler.shipsToPlace.Count > 0)
            {
                int x = (int)((MouseInput.GetScreenPosition(screen).X - 20) / 64) * 64 + 20; //  - (int)(aircraftCarrier.GetCenter().X * 64)
                int y = (int)((MouseInput.GetScreenPosition(screen).Y + 6 ) / 64) * 64 - 6;  //  - (int)(aircraftCarrier.GetCenter().Y * 64)

                Handler.shipsToPlace[0].position = new Vector2(x, y);


                float PositionX = Main.ScreenWidth / 2 - (177 * 4 + 177 * 4 + 40) / 2;
                float PositionY = Main.ScreenHeight / 2 - (177 * 4) / 2;
                Vector2 pos = new Vector2(PositionX + 177 * 4 + 40, PositionY);


                if (KeyInput.isSimpleClick(Keys.Right))
                {
                    Handler.shipsToPlace[0].RotateRight();
                }

                if (MouseInput.isSimpleClickLeft())
                {

                    float PosX = Main.ScreenWidth / 2 - (177 * 4 + 177 * 4 + 40) / 2;
                    float PosY = Main.ScreenHeight / 2 - (177 * 4) / 2;
                    Vector2 pos2 = new Vector2(PosX + 177 * 4 + 40, PosY);

                    for (int i = 0; i < shipsCase.GetLength(0); i++)
                    {
                        for (int j = 0; j < shipsCase.GetLength(1); j++)
                        {
                            if ((new Rectangle((int)pos2.X + (i + 1) * 64, (int)pos2.Y + (j + 1) * 64, 64, 64)).Intersects(MouseInput.GetRectangle(screen)))
                            {

                                if (CanPlaceShip(shipsCase, Handler.shipsToPlace[0], i, j))
                                {
                                    PlaceShip(Handler.shipsToPlace[0], i, j);
                                }

                            }

                        }
                    }

                }


            }


            if (MouseInput.isSimpleClickLeft() && IsAllShipArePlaced())
            {
                for (int i = 0; i < cases.GetLength(0); i++)
                {
                    for (int j = 0; j < cases.GetLength(1); j++)
                    {
                        
                        if (cases[i, j].Intersects(MouseInput.GetRectangle(screen)))
                        {

                            // SetTextLCD("attaque en " + (i + 1) + "" + (char)('a' + (j)));

                            if (!NetPlay.IsMultiplaying)
                            {

                                if (attackCase[i, j] == 0)
                                { attackCase[i, j] = 1; Console.WriteLine("start 1 : " + i + " , " + j); }

                                else if (attackCase[i, j] == 1)
                                { attackCase[i, j] = 2; Console.WriteLine("start 2 : " + i + " , " + j); }

                                else if (attackCase[i, j] == 2)
                                { attackCase[i, j] = 0; Console.WriteLine("start 0 : " + i + " , " + j); }

                            }
                            else
                            {
                                if (NetPlay.MyPlayerID() == 2 && Handler.playerTurn == 2)
                                {
                                    Handler.playerTurn = 1;
                                    ClientSender.SendTarget(i, j);
                                    SetTextLCD("attaque en " + (i + 1) + "" + (char)('a' + (j)));
                                }

                                if (NetPlay.MyPlayerID() == 1 && Handler.playerTurn == 1)
                                {
                                    Handler.playerTurn = 2;
                                    ServerSender.SendTarget(i, j);
                                    SetTextLCD("attaque en " + (i + 1) + "" + (char)('a' + (j)));
                                }

                            }

                        }

                    }
                }

            }

            if (MouseInput.isSimpleClickRight())
            {
                for (int i = 0; i < cases.GetLength(0); i++)
                {
                    for (int j = 0; j < cases.GetLength(1); j++)
                    {
                        if (cases[i, j].Intersects(MouseInput.GetRectangle(screen)))
                        {
                            attackCase[i, j] = 0;
                        }

                    }
                }

            }


            if (KeyInput.isSimpleClick(Keys.Space))
            {

                Console.Write("\n");

                for (int i = 0; i < shipsCase.GetLength(0); i++)
                {
                    for (int j = 0; j < shipsCase.GetLength(1); j++)
                    {
                        Console.Write(shipsCase[j, i] + ",");
                    }
                    Console.Write("\n");
                }

            }


            textTime += 1;

            if (textTime == maxTime)
            {
                if (textQueue.Length > 0)
                {
                    textLCD += textQueue[0];
                    textQueue = textQueue.Substring(1);
                }

                textTime = 0;
            }


        }

        public void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {



        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            float Multiplier = 1;

            //if ((float)((float)Main.ScreenWidth / (float)Main.ScreenHeight) == (float)(16.0 / 10.0))
            //{
            //    Multiplier = 1.5f;
            //}


            float scale = 4; // * (int)((1 / Main.ScreenRatioComparedWith1080p) * (Multiplier));

            //if ((float)((float)Main.ScreenWidth / (float)Main.ScreenHeight) == (float)(16.0 / 9.0))
            //{
            //    scale = 2.5f;
            //}

            float Space = 10 * scale;

            float PositionX = Main.ScreenWidth / 2 - (177 * scale + 177 * scale + Space) / 2;
            float PositionY = Main.ScreenHeight / 2 - (177 * scale) / 2;

            spriteBatch.Draw(Main.DashBoard, new Vector2(Main.ScreenWidth / 2 - (200 * 4 / 2), 20), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
            //Writer.DrawText(Main.UltimateFont, textLCD, new Vector2(Main.ScreenWidth / 2 - (200 * 4 / 2), 20), Color.Green, Color.Green, 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f, spriteBatch);
            spriteBatch.DrawString(Main.UltimateFont, textLCD, new Vector2(Main.ScreenWidth / 2 - (200 * 4 / 2) + 40, 35), new Color(0, 220, 70), 0f, Vector2.Zero, 4f, SpriteEffects.None, 0f);

            spriteBatch.Draw(Main.Grid, new Vector2(PositionX, PositionY), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            spriteBatch.Draw(Main.Grid, new Vector2(PositionX + 177 * scale + Space, PositionY), null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            Vector2 vec = Vector2.Zero;
            Vector2 vec2 = Vector2.Zero;
            if (new Rectangle((int)PositionX + (int)(16 * scale) + 1, (int)PositionY + (int)(16 * scale) + 1, (int)(160 * scale) - 1, (int)(160 * scale) - 1).Intersects(MouseInput.GetRectangle(screen)))
            {

                float DiffX = PositionX;
                float DiffY = PositionY;
                vec = new Vector2((MouseInput.GetRectangle(screen).X - DiffX), (MouseInput.GetRectangle(screen).Y - DiffY));
                vec2 = new Vector2((int)(vec.X / (int)(16 * scale)) * (int)(16 * scale) + DiffX, (int)(vec.Y / (int)(16 * scale)) * (int)(16 * scale) + DiffY); //  + (10 * scale)

                spriteBatch.Draw(Main.SelectedCase, vec2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
                //Console.WriteLine("S : " + (PositionX - vec2.X) + " / DiffX : " + DiffX + " / scale : " + scale);

            }


            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {

                    if (attackCase[i, j] == 2)
                        spriteBatch.Draw(Main.CaseIcon, new Vector2(cases[i, j].X, cases[i, j].Y), new Rectangle(0, 0, 17, 17), Color.White, 0f, Vector2.Zero, GetScale(), SpriteEffects.None, 0f);

                    if (attackCase[i, j] == 1)
                        spriteBatch.Draw(Main.CaseIcon, new Vector2(cases[i, j].X, cases[i, j].Y), new Rectangle(17, 0, 17, 17), Color.White, 0f, Vector2.Zero, GetScale(), SpriteEffects.None, 0f);

                }
            }

            Handler.Draw(spriteBatch, gameTime);

            if (Handler.shipsToPlace.Count > 0)
            {
                //int x = (int)((MouseInput.GetScreenPosition(screen).X - 20) / 64) * 64 + 20 - (int)(aircraftCarrier.GetCenter().X * 64);
                //int y = (int)((MouseInput.GetScreenPosition(screen).Y + 6) / 64) * 64 - 6 - (int)(aircraftCarrier.GetCenter().Y * 64);

                //aircraftCarrier.position = new Vector2(x, y);

                float PosX = Main.ScreenWidth / 2 - (177 * 4 + 177 * 4 + 40) / 2;
                float PosY = Main.ScreenHeight / 2 - (177 * 4) / 2;
                Vector2 pos = new Vector2(PosX + 177 * 4 + 40, PosY);

                for (int i = 0; i < shipsCase.GetLength(0); i++)
                {
                    for (int j = 0; j < shipsCase.GetLength(1); j++)
                    {
                        if ((new Rectangle((int)pos.X + (i + 1) * 64, (int)pos.Y + (j + 1) * 64, 64, 64)).Intersects(MouseInput.GetRectangle(screen)))
                        {

                            if (CanPlaceShip(shipsCase, Handler.shipsToPlace[0], i, j))
                            {
                                Handler.shipsToPlace[0].canPlace = true;
                                goto L_end;
                            }
                            else
                                Handler.shipsToPlace[0].canPlace = false;

                        }
                        else
                            Handler.shipsToPlace[0].canPlace = false;

                    }
                }

            L_end:;

                Handler.shipsToPlace[0].Draw(spriteBatch, gameTime);

            }


            float add = 177 * scale + Space;
            if (new Rectangle((int)PositionX + (int)(16 * scale) + (int)add + 1, (int)PositionY + (int)(16 * scale) + 1, (int)(160 * scale) - 1, (int)(160 * scale) - 1).Intersects(MouseInput.GetRectangle(screen)))
            {

                float DiffX = PositionX + add;
                float DiffY = PositionY;
                vec = new Vector2((MouseInput.GetRectangle(screen).X - DiffX), (MouseInput.GetRectangle(screen).Y - DiffY));
                vec2 = new Vector2((int)(vec.X / (int)(16 * scale)) * (int)(16 * scale) + DiffX, (int)(vec.Y / (int)(16 * scale)) * (int)(16 * scale) + DiffY); //  + (10 * scale)

                spriteBatch.Draw(Main.SelectedCase, vec2, null, Color.White, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);

            }



            //for (int i = 0; i < 10; i++)
            //{
            //    for (int j = 0; j < 10; j++)
            //    {

            //        spriteBatch.Draw(Main.Bounds, cases[i, j], new Color(Random.Shared.Next(0, 255), Random.Shared.Next(0, 255), Random.Shared.Next(0, 255)));

            //    }
            //}


        }

        public Vector2 GetPosition()
        {

            float scale = 4;

            float Space = 10 * scale;

            float PositionX = Main.ScreenWidth / 2 - (177 * scale + 177 * scale + Space) / 2;
            float PositionY = Main.ScreenHeight / 2 - (177 * scale) / 2;


            //float Multiplier = 1;

            //if ((float)((float)Main.ScreenWidth / (float)Main.ScreenHeight) == (float)(16.0 / 10.0))
            //{
            //    Multiplier = 1.5f;
            //}


            //float scale = 4; // * (int)((1 / Main.ScreenRatioComparedWith1080p) * (Multiplier));

            ////if ((float)((float)Main.ScreenWidth / (float)Main.ScreenHeight) == (float)(16.0 / 9.0))
            ////{
            ////    scale = 2.5f;
            ////}

            //float Space = 10 * scale;

            //float PositionX = Main.ScreenWidth / 2 - (177 * scale + 177 * scale + Space) / 2;
            //float PositionY = Main.ScreenHeight / 2 - (177 * scale) / 2;

            return new Vector2(PositionX, PositionY);
        }

        public float GetScale()
        {
            return 4;
        }


        public void InitButton()
        {

        }

        /** Verifie si la navire est plaçable à la position x, y dans la grille */
        public bool CanPlaceShip(int[,] grid, ShipBase ship, int x, int y)
        {

            int[,] sg = ship.GetShipCases();

            for (int i = 0; i < sg.GetLength(1); i++)
            {
                for (int j = 0; j < sg.GetLength(0); j++)
                {
                    
                    if (i - ship.GetCenter().X + x < 0) return false;
                    if (j - ship.GetCenter().Y + y < 0) return false;
                    if (i - ship.GetCenter().X + x >= grid.GetLength(0)) return false;
                    if (j - ship.GetCenter().Y + y >= grid.GetLength(1)) return false;
                    if (shipsCase[(int)(i - ship.GetCenter().X + x), (int)(j - ship.GetCenter().Y + y)] != 0) return false;

                }
            }


            return true;

        }

        /** Place le navire dans la grille */
        public void PlaceShip(ShipBase ship, int x, int y)
        {
            int[,] sg = ship.GetShipCases();

            /// Ajoute le navire à la liste des navires placés
            Handler.ships.Add(Handler.shipsToPlace[0]);
            Handler.shipsToPlace[0].isPlaced = true;

            for (int i = 0; i < sg.GetLength(1); i++)
            {
                for (int j = 0; j < sg.GetLength(0); j++)
                {

                    /// Le numéro placé dans la grille = Position du navire dans la liste
                    shipsCase[(int)(i - ship.GetCenter().X + x), (int)(j - ship.GetCenter().Y + y)] = Handler.ships.Count;

                }
            }

            Handler.shipsToPlace.RemoveAt(0);

            if (IsAllShipArePlaced())
            {

                if (!Handler.isEnemyReady)
                    SetTextLCD("attente . . .");

                if (NetPlay.IsMultiplaying && NetPlay.MyPlayerID() != 1)
                    ClientSender.SendAllShipsArePlaced();
            }

        }

        public bool IsAllShipArePlaced()
        {
            return Handler.shipsToPlace.Count == 0;
        }


        public void SetTextLCD(string text)
        {
            textLCD = "";
            textQueue = text;
        }


        public AttackResult Attack(int x, int y)
        {

            if (shipsCase[x, y] != 0)
            {
                Handler.ships[shipsCase[x, y] - 1].PV -= 1;

                if (Handler.ships[shipsCase[x, y] - 1].PV == 0)
                    return AttackResult.Sunk;
                else
                    return AttackResult.Hited;

            }
                
            return AttackResult.Missed;

        }


        public enum AttackResult
        {
            Missed = 0,
            Hited = 1,
            Sunk = 2,
        } 



    }
}
