using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using UTIL;

namespace BattleShip
{

    public sealed class Camera
    {

        public readonly static float MinZ = 1f;
        public readonly static float MaxZ = 2048f;

        private Vector2 position;
        private float z;
        private float baseZ;

        private float aspectRatio;
        private float fieldOfView;

        private Matrix view;
        private Matrix proj;

        public static float Zoom = 0;


        public Vector2 Position
        {
            get { return this.position; }
        }

        public float Z
        {
            get { return this.z; }
        }

        public Matrix View
        {
            get { return this.view; }
        }

        public Matrix Projection
        {
            get { return this.proj; }
        }


        public Matrix _translation;


        public Camera(Screen screen)
        {
            if (screen is null)
            {
                throw new ArgumentNullException("screen");
            }

            this.aspectRatio = (float)screen.Width / screen.Height;
            this.fieldOfView = MathHelper.PiOver2;

            this.position = new Vector2(0, 0);
            this.baseZ = GetZFromHeight(screen.Height);
            this.z = this.baseZ;

            this.UpdateMatrices();

        }


        public void UpdateMatrices()
        {
            Zoom = 1f;

            int Rx = 1920;
            int Ry = 1080;

            //if (Main.PixelPerfect)
            //{ Rx = Main.ResolutionX; Ry = Main.ResolutionY;}


            /// Creation de la Camera.
            _translation = Matrix.CreateLookAt(new Vector3(Position.X - Rx / (2 * Zoom), Position.Y - Ry / (2 * Zoom), 5), new Vector3(Position.X - Rx / (2 * Zoom), Position.Y - Ry / (2 * Zoom), 0), Vector3.Up);

            /// Application du Zoom à la Camera.
            _translation *= Matrix.CreateScale(Zoom * Main.ScreenRatioComparedWith1080p * 5, Zoom * Main.ScreenRatioComparedWith1080p * 5, 1f);

            /// Application de la rotation de la Camera
            _translation *= Matrix.CreateRotationX(0/ 1000) * Matrix.CreateRotationY(0f / 1000) * Matrix.CreateRotationZ(0f);

            /// Creation de la perspective de la Camera à l'ecran.
            _translation *= Matrix.CreatePerspectiveFieldOfView(this.fieldOfView, 1, Camera.MinZ, Camera.MaxZ);

            /// Creation de test
            //_translation *= Matrix.CreateTranslation(1920/4, 0, 0);
            //_translation *= Matrix.CreateFromYawPitchRoll(0f, 0f, -0.3f);

        }


        public float GetZFromHeight(float height)
        {
            return (0.5f * height) / MathF.Tan(0.5f * this.fieldOfView);
        }

        public void MoveZ(float amount)
        {
            this.z += amount;
            this.z = Util.Clamp(this.z, Camera.MinZ, Camera.MaxZ);
        }


        public void PosZ(float pos)
        {
            this.z = pos;
            this.z = Util.Clamp(this.z, Camera.MinZ, Camera.MaxZ);
        }

        public void Follow(Vector2 Pos)
        {
            position = Pos;

        }


        public void zoom2(float num)
        {
            this.z *= num;
            this.z = Util.Clamp(this.z, Camera.MinZ, Camera.MaxZ);
        }


    }
}
