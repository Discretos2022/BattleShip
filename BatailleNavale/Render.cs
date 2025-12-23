using INPUT;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace BattleShip
{
    public class Render
    {
        private bool isDisposed;
        private Game game;
        private SpriteBatch spriteBatch;
        private BasicEffect effect;

        private Matrix Transform;


        private string technique = "RefractAntiRefractionArea";
        private Texture2D displacementTexture;
        private Vector2 displacement;
        private float sampleWavelength = 1f;
        private float frequency = 1;
        private float refractiveIndex = 1f;
        private float refractionSpeed = 0.1f;

        public static Vector2[] LightPosition = new Vector2[100];
        public static float[] LightPositionY = new float[5];
        public static float[] LightR = new float[5];
        public static float[] LightG = new float[5];
        public static float[] LightB = new float[5];
        public static float[] LightIntensity = new float[5];

        public static float EmbiantLightR;
        public static float EmbiantLightG;
        public static float EmbiantLightB;

        public static int NumOfLight;


        public Render(Game game, SpriteBatch spriteBatch)
        {
            if (game is null)
            {
                throw new ArgumentNullException("game");
            }

            this.game = game;

            this.isDisposed = false;

            this.spriteBatch = spriteBatch;

            this.effect = new BasicEffect(this.game.GraphicsDevice);
            this.effect.FogEnabled = false;
            this.effect.TextureEnabled = true;
            this.effect.LightingEnabled = false;
            this.effect.VertexColorEnabled = true;
            this.effect.World = Matrix.Identity;
            this.effect.Projection = Matrix.Identity;
            this.effect.View = Matrix.Identity;


        }


        public void Dispose(bool disposing)
        {
            if (this.isDisposed)
            {
                return;
            }

            this.spriteBatch?.Dispose();

            this.isDisposed = true;
        }


        [ObsoleteAttribute("This property is obsolete. Use NewProperty instead.")]
        public void Begin(Camera camera, bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }


            if(Main.camActived == true)
            {
                if (camera is null)
                {
                    Viewport vp = this.game.GraphicsDevice.Viewport;
                    this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
                    this.effect.View = Matrix.Identity;
                }
                else
                {
                    camera.UpdateMatrices();
                    
                    this.effect.View = camera.View;
                    this.effect.Projection = camera.Projection;
                    
                    //this.effect.CurrentTechnique = Main.refractionEffect.CurrentTechnique;
                }
            }

            //this.Transform = Matrix.CreateScale(2.5f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(01f, 01f, 0f));

            if (Main.camActived)
                this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, null);
            else 
                this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, null);



        }

        [ObsoleteAttribute("This property is obsolete. Use NewProperty instead.")]
        public void Begin2(Camera camera, bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }


            if (Main.camActived == true)
            {
                if (camera is null)
                {
                    Viewport vp = this.game.GraphicsDevice.Viewport;
                    this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
                    this.effect.View = Matrix.Identity;
                }
                else
                {
                    camera.UpdateMatrices();

                    this.effect.View = camera.View;
                    this.effect.Projection = camera.Projection;

                    //this.effect.CurrentTechnique = Main.refractionEffect.CurrentTechnique;
                }
            }

            //this.Transform = Matrix.CreateScale(2.5f, 1f, 1f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(01f, 01f, 0f));

            if (Main.camActived)
                this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, effect);
            else
                this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, null);



        }

        [ObsoleteAttribute("This property is obsolete. Use NewProperty instead.")]
        public void Begin(bool isTextureFileteringEnabled, GameTime gameTime = null)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }

            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
            this.effect.View = Matrix.Identity;


            

            //this.Transform = Matrix.CreateScale(3f, 3f, 0f) * Matrix.CreateRotationZ(0f) * Matrix.CreateTranslation(new Vector3(0f, 0f, 0f));

            //if (gameTime != null)
                //this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, Main.refractionEffect);
            //else
                //this.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullNone, null);


        }


        public void Begin(bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch, Camera camera = null, bool withEffect = true)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }

            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
            this.effect.View = Matrix.Identity;

            if(camera != null)
            {
                camera.UpdateMatrices();
            }

            
            if(camera != null)
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, null, camera._translation);
            else
                spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, null);


        }

        /// <summary>
        /// Discretos 9.7
        /// </summary>
        /// <param name="isTextureFileteringEnabled"></param>
        /// <param name="gameTime"></param>
        /// <param name="spriteBatch"></param>
        /// <param name="camera"></param>
        /// <param name="withEffect"></param>
        public void Begin5(bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch, Camera camera = null, bool withEffect = true)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }

            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
            this.effect.View = Matrix.Identity;

            if (camera != null)
            {
                camera.UpdateMatrices();
            }


            if (camera != null)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, null, camera._translation);
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, sampler, null, RasterizerState.CullCounterClockwise, null);


        }

        public void BeginAdditive(bool isTextureFileteringEnabled, GameTime gameTime, SpriteBatch spriteBatch, Camera camera = null, bool withEffect = true)
        {

            SamplerState sampler = SamplerState.PointClamp;
            if (isTextureFileteringEnabled)
            {
                sampler = SamplerState.LinearClamp;
            }

            Viewport vp = this.game.GraphicsDevice.Viewport;
            this.effect.Projection = Matrix.CreateOrthographicOffCenter(0, vp.Width, vp.Height, 0, 0f, 1f);
            this.effect.View = Matrix.Identity;

            if (camera != null)
            {
                camera.UpdateMatrices();
            }


            if (camera != null)
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, sampler, null, RasterizerState.CullCounterClockwise, null, camera._translation);
            else
                spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive, sampler, null, RasterizerState.CullCounterClockwise, null);


        }

        public void End(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
        }


        public void Draw(Texture2D texture, Vector2 origine, Vector2 position, Color color)
        {
            this.spriteBatch.Draw(texture, position, null, color, 0f, origine, 1f, SpriteEffects.None, 0f);
        }


        public void Draw(Texture2D texture, Rectangle? sourcerectangle, Vector2 origine, Vector2 position, float rotation, Vector2 scale, Color color)
        {
            this.spriteBatch.Draw(texture, position, sourcerectangle, color, rotation, origine, scale, SpriteEffects.FlipVertically, 0f);
        }


        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Rectangle destinationRectangle, Color color)
        {
            this.spriteBatch.Draw(texture, destinationRectangle, sourceRectangle, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }


        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
        }


        public void Draw(Texture2D texture, Vector2 position, Color color)
        {
            spriteBatch.Draw(texture, position, color);
        }


        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color);
        }


        public void Draw(Texture2D texture, Rectangle position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origine, SpriteEffects effects, float layerDepth)
        {
            spriteBatch.Draw(texture, position, sourceRectangle, color, rotation, origine, effects, layerDepth);
        }

        public void DrawRenderTarget(Texture2D texture, Rectangle destinationRectangle, Color color, GameTime gameTime, SpriteBatch spriteBatch, Rectangle sourceRectangle = default, ShaderEffect shader = ShaderEffect.None)
        {
            
            Begin(!Main.PixelPerfect, gameTime, spriteBatch, null, false);

            spriteBatch.Draw(texture, destinationRectangle, null, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            End(spriteBatch);

        }


        public void DrawLine(Texture2D texture, Vector2 pos1, Vector2 pos2, SpriteBatch spriteBatch, Color color, int epaisseur = 1)
        {

            float distance = Vector2.Distance(pos1, pos2);

            float distanceX = pos2.X - pos1.X;
            float distanceY = pos2.Y - pos1.Y;

            float rotation = (float)Math.Atan2(distanceY, distanceX);

            //Console.WriteLine(distanceX + " ; " + distanceY + " ; " + distance + " ; " + rotation);

            spriteBatch.Draw(texture, new Rectangle((int)pos1.X - epaisseur / 2, (int)pos1.Y + epaisseur / 2, (int)distance, epaisseur), null, color, rotation, new Vector2(0, 0.5f), SpriteEffects.None, 0f);

        }

        public void DrawLine(Texture2D texture, Vector2 pos1, float longueur, float rotation, SpriteBatch spriteBatch, Color color, int epaisseur = 1)
        {

            //float distance = Vector2.Distance(pos1, pos2);

            //float distanceX = pos2.X - pos1.X;
            //float distanceY = pos2.Y - pos1.Y;

            //float rotation = (float)Math.Atan2(distanceY, distanceX);

            //Console.WriteLine(distanceX + " ; " + distanceY + " ; " + distance + " ; " + rotation);

            spriteBatch.Draw(texture, new Rectangle((int)pos1.X - epaisseur / 2, (int)pos1.Y + epaisseur / 2, (int)longueur, epaisseur), null, color, rotation, new Vector2(0, 0.5f), SpriteEffects.None, 0f);

        }

        public enum ShaderEffect
        {
            None = 0,
            HeatDistortion = 1,
            Shadow = 2,
            Shadow2 = 3,
            LightRayTracer = 4,
            LightMaskLevel = 5,
            LightMaskBackground = 6,
        }

        //public static float[] LightPositionX = new float[200];
        //public static float[] LightPositionY = new float[200];
        //public static float[] LightR = new float[200];
        //public static float[] LightG = new float[200];
        //public static float[] LightB = new float[200];
        //public static float[] LightIntensity = new float[200];


        public static void AddLight()
        {
            int i;
            if (NumOfLight != 0)
                i = NumOfLight + 1;
            else
                i = 0;

            LightPosition[i] = new Vector2(MouseInput.getMouseState().X, MouseInput.getMouseState().Y);

            NumOfLight += 1;


        }

    }
}
