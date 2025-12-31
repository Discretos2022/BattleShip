using BattleShip;
using BattleShip.NetCore;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetworkEngine_5._0.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRITER;

namespace BatailleNavale.GameState
{
    class CreateServerState : GameStateBase
    {

        public TextBox textBoxPort;
        public ButtonV3 PortButton;

        private ButtonV3 LaunchServer;

        private State serverState = State.SelectPort;

        private int animTime = 0;
        private string stateText = "";
        private Color stateTextColor = Color.White;

        public CreateServerState(Main main) : base(main)
        {

        }

        public override void Initialize()
        {

            InitButton();

        }

        public override void Update(GameTime gameTime, Screen screen)
        {

            if (serverState == State.SelectPort)
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
                { textBoxPort.isSelected = true; }

                #endregion

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


                #region LaunchServerButton

                LaunchServer.Update(gameTime, screen);

                if (LaunchServer.IsSelected())
                    LaunchServer.SetColor(Color.Gray, Color.Black);
                else
                    LaunchServer.SetColor(Color.White, Color.Black);

                if (port > 65535 || Server.GetStatus() == Server.ServerStatus.Starting)
                    LaunchServer.SetColor(Color.DarkGray, Color.Gray);

                if (LaunchServer.IsCliqued() && Server.GetStatus() == Server.ServerStatus.Offline) //  && !Server.IsLaunched()
                {

                    if (port > 65535)
                        textBoxPort.SetColor(Color.Red, Color.Black);
                    else
                    {
                        Server.Start(port, 4, true);
                        NetPlay.usedPlayerID.Clear();
                        Server.SetAcceptConnection(true);

                        textBoxPort.SetColor(Color.White, Color.Black);

                    }

                }

                #endregion

                if (Server.GetStatus() == Server.ServerStatus.Online)
                {
                    stateText = string.Empty;
                    serverState = State.WaitPlayer;
                    NetPlay.IsMultiplaying = true;

                }

            }
            else if (serverState == State.WaitPlayer)
            {

                if (Server.GetClients().Count == 2)
                {
                    ServerSender.SendStartGame();
                    // Main.gameState = GameState.Playing;
                    main.stateManager.SetState(new PlayState(main));
                    Handler.Initialize();
                }

            }

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            if (serverState == State.SelectPort)
            {

                Writer.DrawText(Main.UltimateFont, "multiplayer settings", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("multiplayer settings").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);

                if (Server.GetStatus() == Server.ServerStatus.Starting)
                {

                    stateTextColor = Color.White;

                    animTime += 1;

                    if (animTime == 60)
                        animTime = 0;

                    if (animTime >= 0 && animTime < 20)
                        stateText = "starting.";
                    else if (animTime >= 20 && animTime < 40)
                        stateText = "starting..";
                    else if (animTime >= 40 && animTime < 60)
                        stateText = "starting...";

                }

                Writer.DrawText(Main.UltimateFont, stateText, new Vector2(10, 5), Color.Black, stateTextColor, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);


                PortButton.Draw(spriteBatch);
                textBoxPort.Draw(spriteBatch);

                LaunchServer.Draw(spriteBatch);
            }

            else if (serverState == State.WaitPlayer)
            {
                Writer.DrawText(Main.UltimateFont, "waiting player ii", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("waiting player ii").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);
            }

        }

        public override void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void Dispose()
        {

        }



        public void InitButton()
        {

            textBoxPort = new TextBox();
            PortButton = new ButtonV3();

            LaunchServer = new ButtonV3();

            textBoxPort = new TextBox(5, 4, 4, false, "7777", true, true);
            textBoxPort.SetPosition(0, 302, ButtonV3.Position.centerX);
            textBoxPort.Update();

            PortButton.SetTexture(Main.PortBox, new Rectangle(0, 0, 52, 16));
            PortButton.SetColor(Color.White, Color.Black);
            PortButton.SetFont(Main.UltimateFont);
            PortButton.SetScale(5);
            PortButton.SetFrontThickness(4);
            PortButton.SetAroundButton();
            PortButton.SetPosition(0, 300, ButtonV3.Position.centerX);

            LaunchServer.SetText("start");
            LaunchServer.SetColor(Color.White, Color.Black);
            LaunchServer.SetFont(Main.UltimateFont);
            LaunchServer.SetScale(5);
            LaunchServer.IsMajuscule(false);
            LaunchServer.SetFrontThickness(4);
            //LaunchServer.SetAroundButton(Back, Back);
            LaunchServer.SetPosition(0, 600, ButtonV3.Position.centerX);

        }

        public enum State
        {
            SelectMode = 0,  /// pas sûr
            SelectPort = 1,
            WaitPlayer = 2,
        };

    }
}
