using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    class State
    {

        public SpriteBatch spriteBatch;
        private Main main;

        public State(SpriteBatch spriteBatch, Main game)
        {
            this.spriteBatch = spriteBatch;
            this.main = game;
        }


        public void Update(GameState state, GameTime gameTime, Screen screen, Main main)
        {
            switch (state)
            {
                case GameState.Menu:
                    Menu.Instance.Update(state, gameTime, screen);
                    break;
                //case GameState.Settings:
                //    settings.Update(state, gameTime, screen, main);
                //    break;
                case GameState.Playing:
                    Play.Instance.Update(gameTime, screen);
                    break;
                case GameState.CreateServer:
                    CreateServer.Instance.Update(state, gameTime, screen);
                    break;
                case GameState.MultiplayerMode:
                    MultiplayerMode.Instance.Update(state, gameTime, screen);
                    break;
                case GameState.ConnectToServer:
                    JoinServer.Instance.Update(state, gameTime, screen);
                    break;
                //case GameState.Multiplaying:
                //    multiplay.Update(state, gameTime, screen);
                //    break;
                //case GameState.MultiplayerMode:
                //    multiplayerMode.Update(state, gameTime, screen);
                //    break;
                //case GameState.CreateServer:
                //    createServer.Update(state, gameTime, screen);
                //    break;
                //case GameState.ConnectToServer:
                //    connectServer.Update(state, gameTime, screen);
                //    break;


            }
        }

        public void Draw(SpriteBatch spriteBatch, GameState state, GameTime gameTime, Screen screen)
        {
            switch (state)
            {
                case GameState.Menu:
                    Menu.Instance.Draw(spriteBatch);
                    break;
                //case GameState.Settings:
                //    settings.Draw(spriteBatch, gameTime);
                //    break;
                case GameState.Playing:
                    Play.Instance.Draw(spriteBatch, gameTime, screen);
                    break;
                case GameState.CreateServer:
                    CreateServer.Instance.Draw(spriteBatch);
                    break;
                case GameState.MultiplayerMode:
                    MultiplayerMode.Instance.Draw(spriteBatch);
                    break;
                case GameState.ConnectToServer:
                    JoinServer.Instance.Draw(spriteBatch);
                    break;
                //case GameState.Multiplaying:
                //    multiplay.Draw(spriteBatch, gameTime);
                //    break;
                //case GameState.MultiplayerMode:
                //    multiplayerMode.Draw(spriteBatch, gameTime);
                //    break;
                //case GameState.CreateServer:
                //    createServer.Draw(spriteBatch, gameTime);
                //    break;
                //case GameState.ConnectToServer:
                //    connectServer.Draw(spriteBatch, gameTime);
                //    break;

            }


        }

        public void DrawInCamera(SpriteBatch spriteBatch, GameState state, GameTime gameTime)
        {
            switch (state)
            {
                case GameState.Menu:
                    break;
                case GameState.Settings:
                    break;
                case GameState.Playing:
                    Play.Instance.DrawInCamera(spriteBatch, gameTime);
                    break;
                //case GameState.Multiplaying:
                //    multiplay.DrawInCamera(spriteBatch, gameTime);
                //    break;
                case GameState.MultiplayerMode:
                    break;
                case GameState.CreateServer:
                    break;
                case GameState.ConnectToServer:
                    break;

            }
        }


    }

    public enum GameState { Menu, Settings, Playing, MultiplayerMode, CreateServer, ConnectToServer, Multiplaying }

    public enum PlayState { InWorldMap, InLevel }

}
