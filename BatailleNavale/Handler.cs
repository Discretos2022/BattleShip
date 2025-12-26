using BatailleNavale;
using BattleShip.NetCore;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BattleShip
{
    static class Handler
    {

        //public static List<Actor> actors = new List<Actor>();

        //public static TileV2[,] Level;

        public static List<ShipBase> ships = new List<ShipBase>();

        public static List<ShipBase> shipsToPlace = new List<ShipBase>();

        public static bool isEnemyReady = false;
        public static int playerTurn = 0;

        public static void Initialize()
        {
            //solids = new List<Solid>();

            ships = new List<ShipBase>();

            if (!NetPlay.IsMultiplaying)
                isEnemyReady = true;

            shipsToPlace.Add(new AircraftCarrier(new Vector2(0, 0)));
            shipsToPlace.Add(new Submarine(new Vector2(0, 0)));
            shipsToPlace.Add(new Destroyer(new Vector2(0, 0)));
            shipsToPlace.Add(new Destroyer(new Vector2(0, 0)));
            shipsToPlace.Add(new PatrolBoat(new Vector2(0, 0)));

        }

        public static void Update(GameTime gameTime)
        {

            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].Update(gameTime);
            }

        }

        public static void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            for (int i = 0; i < ships.Count; i++)
            {
                ships[i].Draw(spriteBatch, gameTime);
            }

        }


    }
}
