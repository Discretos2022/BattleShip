using BatailleNavale;
using BattleShip.NetCore;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using NetworkEngine_5._0.Client;
using NetworkEngine_5._0.Error;
using NetworkEngine_5._0.Server;
using System;
using System.Collections.Generic;
using WRITER;

namespace BattleShip
{
    class JoinServer
    {

        private Main main;

        private Color grayColor = new Color(60, 60, 60);

        public TextBox textBoxPort;
        public ButtonV3 PortButton;

        public TextBox textBoxIP;
        public ButtonV3 IPButton;

        private ButtonV3 joinServer;

        private State joinState = State.SelectHost;

        private int animTime = 0;
        private string stateText = "";
        private Color stateTextColor = Color.White;


        public JoinServer(Main main)
        {

            this.main = main;

            textBoxPort = new TextBox();
            PortButton = new ButtonV3();

            textBoxIP = new TextBox();
            IPButton = new ButtonV3();

            joinServer = new ButtonV3();

            InitButton();


        }

        public void Update(GameState state, GameTime gameTime, Screen screen)
        {

            if (joinState == State.SelectHost)
            {
                #region PortButton

                PortButton.Update(gameTime, screen);

                if (PortButton.IsSelected())
                    PortButton.SetTexture(Main.PortBox, new Rectangle(53, 0, 52, 16));
                else
                {
                    if (textBoxPort.isSelected)
                        PortButton.SetTexture(Main.PortBox, new Rectangle(106, 0, 52, 16));
                    else
                        PortButton.SetTexture(Main.PortBox, new Rectangle(0, 0, 52, 16));
                }


                if (PortButton.IsCliqued())
                { textBoxPort.isSelected = true; textBoxIP.isSelected = false; }

                #endregion

                #region PortBox

                int port = 0;

                if (textBoxPort.GetText().Length == 0)
                    port = 7777;
                else
                    port = int.Parse(textBoxPort.GetText());

                if (port > 65535)
                    textBoxPort.SetColor(Color.Red, Color.Black);
                else
                    textBoxPort.SetColor(Color.White, Color.Black);

                if (textBoxPort.isSelected)
                    textBoxPort.Update();

                if (MouseInput.isSimpleClickLeft())
                {
                    if (!PortButton.IsSelected())
                        textBoxPort.isSelected = false;
                }

                #endregion

                #region IPButton

                IPButton.Update(gameTime, screen);

                if (IPButton.IsSelected())
                    IPButton.SetTexture(Main.IPBox, new Rectangle(121, 0, 120, 16));
                else
                {
                    if (textBoxIP.isSelected)
                        IPButton.SetTexture(Main.IPBox, new Rectangle(242, 0, 120, 16));
                    else
                        IPButton.SetTexture(Main.IPBox, new Rectangle(0, 0, 120, 16));
                }


                if (IPButton.IsCliqued())
                { textBoxIP.isSelected = true; textBoxPort.isSelected = false; }

                #endregion

                #region IPBox

                if (!IsValidIP(textBoxIP.GetText()))
                    textBoxIP.SetColor(Color.Red, Color.Black);
                else
                    textBoxIP.SetColor(Color.White, Color.Black);

                if (textBoxIP.isSelected)
                    textBoxIP.Update();

                if (MouseInput.isSimpleClickLeft())
                {
                    if (!IPButton.IsSelected())
                        textBoxIP.isSelected = false;
                }

                #endregion


                #region JoinServerButton

                joinServer.Update(gameTime, screen);

                if (joinServer.IsSelected())
                    joinServer.SetColor(Color.Gray, Color.Black);
                else
                    joinServer.SetColor(Color.White, Color.Black);

                if (port > 65535 || !IsValidIP(textBoxIP.GetText()) || Client.GetState() == Client.ClientState.Connecting)
                    joinServer.SetColor(Color.DarkGray, Color.Gray);

                if (joinServer.IsCliqued() && Client.GetState() == Client.ClientState.Disconnected) //  && !Server.IsLaunched()
                {

                    if (port <= 65535 && IsValidIP(textBoxIP.GetText())) 
                    {

                        Connection(textBoxIP.GetText(), port);
                        NetPlay.usedPlayerID.Clear();
                        Server.SetAcceptConnection(true);

                        textBoxPort.SetColor(Color.White, Color.Black);

                    }

                }

                #endregion


                if (Client.GetState() == Client.ClientState.Connected)
                {
                    stateText = string.Empty;
                    joinState = State.WaitPlayer;
                    NetPlay.IsMultiplaying = true;

                }
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            if (joinState == State.SelectHost)
            {

                Writer.DrawText(Main.UltimateFont, "multiplayer settings", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("multiplayer settings").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);

                if (Client.GetState() == Client.ClientState.Connecting)
                {

                    stateTextColor = Color.White;

                    animTime += 1;

                    if (animTime == 60)
                        animTime = 0;

                    if (animTime >= 0 && animTime < 20)
                        stateText = "connecting.";
                    else if (animTime >= 20 && animTime < 40)
                        stateText = "connecting..";
                    else if (animTime >= 40 && animTime < 60)
                        stateText = "connecting...";

                }

                Writer.DrawText(Main.UltimateFont, stateText, new Vector2(10, 5), Color.Black, stateTextColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);


                PortButton.Draw(spriteBatch);
                textBoxPort.Draw(spriteBatch);

                IPButton.Draw(spriteBatch);
                textBoxIP.Draw(spriteBatch);

                joinServer.Draw(spriteBatch);
            }

            else if (joinState == State.WaitPlayer) {
                Writer.DrawText(Main.UltimateFont, "waiting player i", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("waiting player i").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);
            }


        }


        public void InitButton()
        {

            textBoxPort = new TextBox(5, 4, 4, false, "7777", true, true);
            textBoxPort.SetPosition(0, 402, ButtonV3.Position.centerX);
            textBoxPort.Update();

            PortButton.SetTexture(Main.PortBox, new Rectangle(0, 0, 52, 16));
            PortButton.SetColor(Color.White, Color.Black);
            PortButton.SetFont(Main.UltimateFont);
            PortButton.SetScale(5);
            PortButton.SetFrontThickness(4);
            PortButton.SetAroundButton();
            PortButton.SetPosition(0, 400, ButtonV3.Position.centerX);


            textBoxIP = new TextBox(15, 4, 4, false, "192.168.1.25", true, false);
            textBoxIP.SetPosition(0, 302, ButtonV3.Position.centerX);
            textBoxIP.Update();

            IPButton.SetTexture(Main.IPBox, new Rectangle(0, 0, 120, 16));
            IPButton.SetColor(Color.White, Color.Black);
            IPButton.SetFont(Main.UltimateFont);
            IPButton.SetScale(5);
            IPButton.SetFrontThickness(4);
            IPButton.SetAroundButton();
            IPButton.SetPosition(0, 300, ButtonV3.Position.centerX);


            joinServer.SetText("start");
            joinServer.SetColor(Color.White, Color.Black);
            joinServer.SetFont(Main.UltimateFont);
            joinServer.SetScale(5);
            joinServer.IsMajuscule(false);
            joinServer.SetFrontThickness(4);
            //LaunchServer.SetAroundButton(Back, Back);
            joinServer.SetPosition(0, 600, ButtonV3.Position.centerX);

        }

        public enum State
        {
            SelectHost = 0,
            WaitPlayer = 1,
        };

        public bool IsValidIP(string IP)
        {
            int num = 0;
            int point = 0;

            if (IP.Length < 7) return false;

            if (IP.ToCharArray()[0] == '.' || IP.ToCharArray()[IP.Length - 1] == '.') return false;

            for (int i = 0; i < IP.Length; i++)
            {
                if (IP.ToCharArray()[i] == '.')
                    point += 1;

                if (char.IsDigit(IP.ToCharArray()[i]))
                    num += 1;

            }

            if (num < 4) return false;
            if (point < 3) return false;

            point = 0;
            num = 0;

            for (int i = 0; i < IP.Length; i++)
            {
                if (char.IsDigit(IP.ToCharArray()[i]))
                { num += 1; point = 0; }

                if (IP.ToCharArray()[i] == '.')
                { point += 1; num = 0; }

                if (point > 1 || num > 3)
                    return false;

            }
            return true;
        }

        /// <summary>
        /// Sous-tache async pour récup l'erreur !
        /// </summary>
        /// <param name="IP"></param>
        /// <param name="port"></param>
        public async void Connection(string IP, int port)
        {
            try
            {
                await Client.Connect(IP, port);
            }
            catch (ConnectionError)
            {
                stateTextColor = Color.Red;
                stateText = "connection failed";
            }
            catch (FullError)
            {
                stateTextColor = Color.Red;
                stateText = "max player is reached";
            }
            catch (RefuseError)
            {
                stateTextColor = Color.Red;
                stateText = "game was already started";
            }



        }

    }
}