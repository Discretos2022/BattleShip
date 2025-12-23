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
        private Menu menu;
        private Play play;
        private MultiplayerMode multiplayerMode;
        private CreateServer createServer;
        private JoinServer joinServer;
        //private Settings settings;
        //private MultiplayerMode multiplayerMode;
        //private CreateServer createServer;
        //private ConnectServer connectServer;
        //private Multiplay multiplay;

        public State(SpriteBatch spriteBatch, Menu menu, Play play, CreateServer createServer, MultiplayerMode multiplayerMode, JoinServer joinServer, /*Settings settings, Play play, MultiplayerMode multiplayerMode, CreateServer createServer, ConnectServer connectServer, Multiplay multiplay,*/ Main game)
        {
            this.spriteBatch = spriteBatch;
            this.main = game;
            this.menu = menu;
            this.play = play;
            this.multiplayerMode = multiplayerMode;
            this.createServer = createServer;
            this.joinServer = joinServer;
            //this.settings = settings;
            //this.multiplayerMode = multiplayerMode;
            //this.createServer = createServer;
            //this.connectServer = connectServer;
            //this.multiplay = multiplay;
        }


        public void Update(GameState state, GameTime gameTime, Screen screen, Main main)
        {
            switch (state)
            {
                case GameState.Menu:
                    menu.Update(state, gameTime, screen);
                    break;
                //case GameState.Settings:
                //    settings.Update(state, gameTime, screen, main);
                //    break;
                case GameState.Playing:
                    play.Update(gameTime, screen);
                    break;
                case GameState.CreateServer:
                    createServer.Update(state, gameTime, screen);
                    break;
                case GameState.MultiplayerMode:
                    multiplayerMode.Update(state, gameTime, screen);
                    break;
                case GameState.ConnectToServer:
                    joinServer.Update(state, gameTime, screen);
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
                    menu.Draw(spriteBatch);
                    break;
                //case GameState.Settings:
                //    settings.Draw(spriteBatch, gameTime);
                //    break;
                case GameState.Playing:
                    play.Draw(spriteBatch, gameTime, screen);
                    break;
                case GameState.CreateServer:
                    createServer.Draw(spriteBatch);
                    break;
                case GameState.MultiplayerMode:
                    multiplayerMode.Draw(spriteBatch);
                    break;
                case GameState.ConnectToServer:
                    joinServer.Draw(spriteBatch);
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
                    play.DrawInCamera(spriteBatch, gameTime);
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
