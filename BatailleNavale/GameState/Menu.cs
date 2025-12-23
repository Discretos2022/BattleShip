using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using WRITER;

namespace BattleShip
{
    class Menu
    {

        private Main main;

        private ButtonV3 Classique;
        private ButtonV3 Network;
        //private ButtonV3 Settings2;
        private ButtonV3 Quit;

        private List<ButtonV3> buttons2 = new List<ButtonV3>();

        private int selectedButton = 0;
        private bool useMouse = true;

        private Color grayColor = new Color(60, 60, 60);


        public Menu(Main main)
        {

            this.main = main;

            Classique = new ButtonV3();
            Network = new ButtonV3();
            //Settings2 = new ButtonV3();
            Quit = new ButtonV3();

            buttons2.Add(Classique);
            buttons2.Add(Network);
            //buttons2.Add(Settings2);
            buttons2.Add(Quit);

            InitButton();


        }

        public void Update(GameState state, GameTime gameTime, Screen screen)
        {


            #region SinglePlayerButton2

            Classique.Update(gameTime, screen);

            if (Classique.IsSelected())
                Classique.SetColor(Color.Gray, grayColor);
            else
                Classique.SetColor(Color.White, grayColor);

            if (Classique.IsCliqued())
            {

                //if (Handler.players.Count != 2)
                //{
                //    Handler.AddPlayerV2(1);
                //}


                //Main.inWorldMap = true;
                //Main.inLevel = false;
                //Camera.Zoom = 1f;
                Main.gameState = GameState.Playing;

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
                //Background.SetBackground(3);
                Main.gameState = GameState.MultiplayerMode;
            }

            #endregion


            /*#region SettingsButton2

            Settings2.Update(gameTime, screen);

            if (Settings2.IsSelected())
                Settings2.SetColor(Color.Blue, grayColor);
            else
                Settings2.SetColor(Color.White, grayColor);

            if (Settings2.IsCliqued())
            {
                Main.gameState = GameState.Settings;
                Background.SetBackground(3);

                if (!MouseInput.IsActived)
                    ButtonManager.settingsButtons[0].SetIsSelected(true);

            }


            #endregion*/


            #region QuitButton2

            Quit.Update(gameTime, screen);

            if (Quit.IsSelected())
                Quit.SetColor(Color.Gold, grayColor);
            else
                Quit.SetColor(Color.White, grayColor);

            if (Quit.IsCliqued())
                main.QuitGame();

            #endregion


            if (KeyInput.isSimpleClick(Keys.Up, Keys.Down) || GamePadInput.isSimpleClick(PlayerIndex.One, Buttons.DPadUp, Buttons.DPadDown))
            {
                for (int i = 0; i < buttons2.Count; i++)
                {
                    if (buttons2[i].IsSelected())
                        goto L_2;

                }

                Classique.SetIsSelected(true);
                MouseInput.IsActived = false;

            L_2:;
            }


        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(Main.Banner, new Vector2(1920 / 2 - Main.Banner.Width * 8 / 2, 25), null, Color.White, 0f, new Vector2(0, 0), 8, SpriteEffects.None, 0f);
            //spriteBatch.Draw(Main.Screen1, new Vector2(0, 0), null, Color.White, 0f, new Vector2(0, 0), 4f, SpriteEffects.None, 0f);

            //Writer.DrawText(Main.UltimateFont, "version " + Main.Version + " build " + Main.Build + " " + Main.State + " " + Main.Platform, new Vector2(10, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);
            //Writer.DrawText(Main.UltimateFont, "ip : " + Main.IP, new Vector2(1640, 1040), Color.Black, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0f, 2f, spriteBatch);

            Classique.SetScale(4);
            Classique.SetFrontThickness(3);
            Classique.SetPosition(0, 240, ButtonV3.Position.centerX);

            Network.SetScale(4);
            Network.SetFrontThickness(3);
            Network.SetPosition(0, 320, ButtonV3.Position.centerX);

            //Settings2.SetScale(4 * (int)(1 / Main.ScreenRatioComparedWith1080p));
            //Settings2.SetFrontThickness(4 * (int)(1 / Main.ScreenRatioComparedWith1080p) - 1);
            //Settings2.SetPosition(0, 400 * (int)(1 / Main.ScreenRatioComparedWith1080p), ButtonV3.Position.centerX);

            Quit.SetScale(4 * (int)(1 / Main.ScreenRatioComparedWith1080p));
            Quit.SetFrontThickness(4 * (int)(1 / Main.ScreenRatioComparedWith1080p) - 1);
            Quit.SetPosition(0, 400 * (int)(1 / Main.ScreenRatioComparedWith1080p), ButtonV3.Position.centerX);

            Classique.IsMajuscule(false);
            Network.IsMajuscule(false);
            //Settings2.IsMajuscule(false);
            Quit.IsMajuscule(false);

            for (int i = 0; i < buttons2.Count; i++)
            {
                buttons2[i].Draw(spriteBatch, false, true, Color.Black);
            }



        }


        public void InitButton()
        {
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

            //Settings2.SetText("settings");
            //Settings2.SetColor(Color.White, Color.Black);
            //Settings2.SetFont(Main.UltimateFont);
            //Settings2.SetScale(4);
            //Settings2.IsMajuscule(false);
            //Settings2.SetFrontThickness(3);
            //Settings2.SetAroundButton(Network, Quit);

            Quit.SetText("quit");
            Quit.SetColor(Color.White, new Color(60,60,60));
            Quit.SetFont(Main.UltimateFont);
            Quit.SetScale(4);
            Quit.IsMajuscule(false);
            Quit.SetFrontThickness(3);
            Quit.SetAroundButton(Network, null);


            Classique.SetPosition(0, 340, ButtonV3.Position.centerX);
            Network.SetPosition(0, 440, ButtonV3.Position.centerX);
            //Settings2.SetPosition(0, 540, ButtonV3.Position.centerX);
            Quit.SetPosition(0, 540, ButtonV3.Position.centerX);

        }

    }
}