using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatailleNavale
{
    public abstract class ShipBase
    {

        public Vector2 position;
        public int[,] shipCase;
        public Vector2 center;
        public bool canPlace;
        public bool isPlaced;
        public double angle = 0;

        public ShipBase(Vector2 position) 
        {
            
            this.position = position;

        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(SpriteBatch spriteBatch, GameTime gameTime);

        public abstract int[,] GetShipCases();
        public abstract Vector2 GetCenter();


        public void RotateRight()
        {

            angle += Math.PI / 2;

            Console.WriteLine("\n--------------------------");
            Console.WriteLine(center);

            if (angle == Math.PI / 2)
                center = new Vector2(center.Y, center.X);
            else if (angle == Math.PI)
                center = new Vector2(shipCase.GetLength(0) - 1 - center.Y, center.X);
            else if (angle == Math.PI + Math.PI / 2)
                center = new Vector2(center.Y, center.X);
            else if (angle == Math.PI * 2)
                center = new Vector2(shipCase.GetLength(0) - 1 - center.Y, center.X);

            Console.WriteLine(center);

            if (angle == 2 * Math.PI) angle = 0;
            if (angle < 0) angle = Math.PI + Math.PI / 2;


            int[,] newGrid = new int[shipCase.GetLength(1), shipCase.GetLength(0)];

            for (int i = 0; i < newGrid.GetLength(0); i++)
            {
                for (int j = 0; j < newGrid.GetLength(1); j++)
                {
                    newGrid[i, j] = shipCase[j, shipCase.GetLength(1) - i - 1];
                }
            }


            shipCase = newGrid;

        }

    }
}
