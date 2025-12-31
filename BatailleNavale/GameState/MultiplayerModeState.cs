using BattleShip;
using BattleShip.NetCore;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using NetworkEngine_5._0.Client;
using NetworkEngine_5._0.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WRITER;

namespace BatailleNavale.GameState
{
    class MultiplayerModeState : GameStateBase
    {

        private ButtonV3 createServer;
        private ButtonV3 joinServer;

        private Color grayColor = new Color(60, 60, 60);

        public MultiplayerModeState(Main main) : base(main)
        {

        }

        public override void Initialize()
        {

            InitButton();

        }

        public override void Update(GameTime gameTime, Screen screen)
        {

            #region createServerButton

            createServer.Update(gameTime, screen);

            if (createServer.IsSelected())
                createServer.SetColor(Color.Red, grayColor);
            else
                createServer.SetColor(Color.White, grayColor);

            if (createServer.IsCliqued())
            {
                // Main.gameState = GameState.CreateServer;
                main.stateManager.SetState(new CreateServerState(main));
            }

            #endregion

            #region joinServerButton

            joinServer.Update(gameTime, screen);

            if (joinServer.IsSelected())
                joinServer.SetColor(Color.Red, grayColor);
            else
                joinServer.SetColor(Color.White, grayColor);

            if (joinServer.IsCliqued())
            {
                // Main.gameState = GameState.ConnectToServer;
                main.stateManager.SetState(new JoinServerState(main));
            }

            #endregion

        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            Writer.DrawText(Main.UltimateFont, "multiplayer", new Vector2((1920 / 2) - (Main.UltimateFont.MeasureString("multiplayer").X * 8f + 9 * 8f) / 2, 25 - 15), new Color(60, 60, 60), Color.LightGray, 0f, Vector2.Zero, 8f, SpriteEffects.None, 0f, 6f, spriteBatch, Color.Black, false);

            createServer.Draw(spriteBatch);
            joinServer.Draw(spriteBatch);

        }

        public override void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {

        }

        public override void Dispose()
        {

        }



        public void InitButton()
        {

            createServer = new ButtonV3();
            joinServer = new ButtonV3();

            createServer.SetText("create a party");
            createServer.SetColor(Color.White, Color.Black);
            createServer.SetFont(Main.UltimateFont);
            createServer.SetScale(4);
            createServer.IsMajuscule(false);
            createServer.SetFrontThickness(3);
            createServer.SetAroundButton();
            createServer.SetPosition(0, 500, ButtonV3.Position.centerX);

            joinServer.SetText("join a party");
            joinServer.SetColor(Color.White, Color.Black);
            joinServer.SetFont(Main.UltimateFont);
            joinServer.SetScale(4);
            joinServer.IsMajuscule(false);
            joinServer.SetFrontThickness(3);
            joinServer.SetAroundButton();
            joinServer.SetPosition(0, 600, ButtonV3.Position.centerX);

        }

    }
}
