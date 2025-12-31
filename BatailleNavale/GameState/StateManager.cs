using BattleShip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale.GameState
{
    public class StateManager
    {

        private GameStateBase currentState;

        public StateManager(GameStateBase initialState)
        {
            currentState = initialState;
            currentState.Initialize();
        }

        public void Update(GameTime gameTime, Screen screen)
        {
            currentState.Update(gameTime, screen);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen)
        {
            currentState.Draw(spriteBatch, gameTime, screen);
        }

        public void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime)
        {
            currentState.DrawInCamera(spriteBatch, gameTime);
        }

        public void SetState(GameStateBase state)
        {
            currentState.Dispose();
            currentState = state;
            currentState.Initialize();
        }


    }
}
