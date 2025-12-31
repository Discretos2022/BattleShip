using BattleShip;
using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale.GameState
{
    class MainMenu : GameStateBase
    {

        private ButtonV3 Classique;
        private ButtonV3 Network;
        private ButtonV3 Quit;

        private Color grayColor = new Color(60, 60, 60);

        public MainMenu(Main main) : base(main)
        {

        }

        public override void Initialize()
        {

            InitButton();

        }

        public override void Update(GameTime gameTime, Screen screen)
        {

            #region SinglePlayerButton2

            Classique.Update(gameTime, screen);

            if (Classique.IsSelected())
                Classique.SetColor(Color.Gray, grayColor);
            else
                Classique.SetColor(Color.White, grayColor);

            if (Classique.IsCliqued())
            {
                // Main.gameState = GameState.Playing;
                main.stateManager.SetState(new PlayState(main));
                Handler.Initialize();
            }

            #endregion


            #region MultiPlayerButton2

            Network.Update(gameTime, screen);

            if (Network.IsSelected())
                Network.SetColor(Color.Red, grayColor);
            else
                Network.SetColor(Color.White, grayColor);

            if (Network.IsCliqued())
            {
                // Main.gameState = GameState.MultiplayerMode;
                main.stateManager.SetState(new MultiplayerModeState(main));
            }

            #endregion


            //if (KeyInput.isSimpleClick(Keys.Up, Keys.Down) || GamePadInput.isSimpleClick(PlayerIndex.One, Buttons.DPadUp, Buttons.DPadDown))
            //{
            //    for (int i = 0; i < buttons2.Count; i++)
            //    {
            //        if (buttons2[i].IsSelected())
            //            goto L_2;

            //    }

            //    Classique.SetIsSelected(true);
            //    MouseInput.IsActived = false;

            //L_2:;
            //}


        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {

            spriteBatch.Draw(Main.Banner, new Vector2(1920 / 2 - Main.Banner.Width * 8 / 2, 25), null, Color.White, 0f, new Vector2(0, 0), 8, SpriteEffects.None, 0f);

            Classique.SetScale(4);
            Classique.SetFrontThickness(3);
            Classique.SetPosition(0, 240, ButtonV3.Position.centerX);

            Network.SetScale(4);
            Network.SetFrontThickness(3);
            Network.SetPosition(0, 320, ButtonV3.Position.centerX);

            Quit.SetScale(4 * (int)(1 / Main.ScreenRatioComparedWith1080p));
            Quit.SetFrontThickness(4 * (int)(1 / Main.ScreenRatioComparedWith1080p) - 1);
            Quit.SetPosition(0, 400 * (int)(1 / Main.ScreenRatioComparedWith1080p), ButtonV3.Position.centerX);

            Classique.IsMajuscule(false);
            Network.IsMajuscule(false);
            Quit.IsMajuscule(false);

            Classique.Draw(spriteBatch, false, true, Color.Black);
            Network.Draw(spriteBatch, false, true, Color.Black);
            Quit.Draw(spriteBatch, false, true, Color.Black);


        }

        public override void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {
            
        }

        public override void Dispose()
        {
            
        }



        public void InitButton()
        {

            Classique = new ButtonV3();
            Network = new ButtonV3();
            Quit = new ButtonV3();

            Classique.SetText("classique");
            Classique.SetColor(Color.White, Color.Black);
            Classique.SetFont(Main.UltimateFont);
            Classique.SetScale(4);
            Classique.IsMajuscule(false);
            Classique.SetFrontThickness(3);
            Classique.SetAroundButton(null, Network);

            Network.SetText("network");
            Network.SetColor(Color.White, Color.Black);
            Network.SetFont(Main.UltimateFont);
            Network.SetScale(4);
            Network.IsMajuscule(false);
            Network.SetFrontThickness(3);
            Network.SetAroundButton(Classique, Quit);

            Quit.SetText("quit");
            Quit.SetColor(Color.White, new Color(60, 60, 60));
            Quit.SetFont(Main.UltimateFont);
            Quit.SetScale(4);
            Quit.IsMajuscule(false);
            Quit.SetFrontThickness(3);
            Quit.SetAroundButton(Network, null);


            Classique.SetPosition(0, 340, ButtonV3.Position.centerX);
            Network.SetPosition(0, 440, ButtonV3.Position.centerX);
            Quit.SetPosition(0, 540, ButtonV3.Position.centerX);

        }


    }
}
