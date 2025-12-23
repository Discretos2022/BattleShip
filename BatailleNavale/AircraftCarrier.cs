using BattleShip;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    public class AircraftCarrier : ShipBase
    {
        public AircraftCarrier(Vector2 position) : base(position)
        {

            shipCase = new int[,]
            {
                { 1, 1, 1, 1, 1 },
            };

            center = new Vector2(2, 0);

        }

        public override void Update(GameTime gameTime)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {

            int fixPlacementRotationX = 0;
            int fixPlacementRotationY = 0;

            if (angle == Math.PI / 2 || angle == Math.PI) fixPlacementRotationX = 4;
            if (angle == Math.PI || angle == Math.PI + Math.PI / 2) fixPlacementRotationY = 4;

            if (canPlace)
                spriteBatch.Draw(Main.AircraftCarrier, position + new Vector2(32 + fixPlacementRotationX, 32 + fixPlacementRotationY), null, Color.White * 0.5f, (float)angle, new Vector2(79/2, 15/2)/*new Vector2(2 * 16 + 7, 0 * 16 + 7)*/, 4f, SpriteEffects.None, 0f);
            else
                spriteBatch.Draw(Main.AircraftCarrier, position + new Vector2(32 + fixPlacementRotationX, 32 + fixPlacementRotationY), null, Color.Red * 0.5f, (float)angle, new Vector2(79 / 2, 15 / 2)/*new Vector2(2 * 16 + 7, 0 * 16 + 7)*/, 4f, SpriteEffects.None, 0f);

        }

        public override Vector2 GetCenter()
        {
            return center;
        }

        public override int[,] GetShipCases()
        {
            return shipCase;
        }

    }
}
