using BattleShip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale.GameState
{
    public abstract class GameStateBase
    {

        protected Main main;

        public GameStateBase(Main main) 
        {
            this.main = main;
        }

        public abstract void Initialize();
        public abstract void Update(GameTime gameTime, Screen screen);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime, Screen screen);
        public abstract void DrawInCamera(SpriteBatch spriteBatch, GameTime gameTime);
        public abstract void Dispose();

    }
}
