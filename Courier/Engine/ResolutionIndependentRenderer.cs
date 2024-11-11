using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

// Modified from source: http://blog.roboblob.com/2013/07/27/solving-resolution-independent-rendering-and-2d-camera-using-monogame/comment-page-1/
namespace Courier.Engine
{
    public class ResolutionIndependentRenderer
    {
        // The virtual resolution we always want our game to display at.
        public const int VirtualHeight = 480;
        public const int VirtualWidth = 960;

        private const int DefaultRealScreenWidth = 1920;
        private const int DefaultRealScreenHeight = 1080;

        public readonly int ScreenWidth;
        public readonly int ScreenHeight;

        private readonly Microsoft.Xna.Framework.Game game;
        private readonly GraphicsDeviceManager graphics;
        private Viewport viewport;

        private static Matrix scaleMatrix;
        private bool dirtyMatrix = true;

        public SpriteBatch SpriteBatch { get; private set; }

        public ResolutionIndependentRenderer(Microsoft.Xna.Framework.Game game)
        {
            this.game = game;
            graphics = new GraphicsDeviceManager(game);

            ScreenWidth = DefaultRealScreenWidth;
            ScreenHeight = DefaultRealScreenHeight;

        }

        public void Initialize()
        {
            graphics.PreferredBackBufferWidth = DefaultRealScreenWidth;
            graphics.PreferredBackBufferHeight = DefaultRealScreenHeight;
            graphics.ApplyChanges();

            SetupVirtualScreenViewport();

            dirtyMatrix = true;
        }

        public void LoadContent()
        {
            SpriteBatch = new SpriteBatch(game.GraphicsDevice);
        }

        public void BeginDraw()
        {
            // Start by reseting viewport to (0,0,1,1)
            SetupFullViewport();
            // Clear to Black
            game.GraphicsDevice.Clear(Color.Black);
            // Calculate Proper Viewport according to Aspect Ratio
            SetupVirtualScreenViewport();

            SpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                GetTransformationMatrix()
            );
        }

        public void EndDraw()
        {
            SpriteBatch.End();
        }

        private Matrix GetTransformationMatrix()
        {
            if (dirtyMatrix)
                RecreateScaleMatrix();

            return scaleMatrix;
        }

        private void RecreateScaleMatrix()
        {
            Matrix.CreateScale((float)ScreenWidth / VirtualWidth, (float)ScreenWidth / VirtualWidth, 1f, out scaleMatrix);
            dirtyMatrix = false;
        }

        private void SetupFullViewport()
        {
            var vp = new Viewport();
            vp.X = vp.Y = 0;
            vp.Width = ScreenWidth;
            vp.Height = ScreenHeight;
            game.GraphicsDevice.Viewport = vp;
            dirtyMatrix = true;
        }

        private void SetupVirtualScreenViewport()
        {
            var targetAspectRatio = VirtualWidth / (float)VirtualHeight;
            // figure out the largest area that fits in this resolution at the desired aspect ratio
            var width = ScreenWidth;
            var height = (int)(width / targetAspectRatio + .5f);

            if (height > ScreenHeight)
            {
                height = ScreenHeight;
                // PillarBox
                width = (int)(height * targetAspectRatio + .5f);
            }

            // set up the new viewport centered in the backbuffer
            viewport = new Viewport
            {
                X = (ScreenWidth / 2) - (width / 2),
                Y = (ScreenHeight / 2) - (height / 2),
                Width = width,
                Height = height
            };

            game.GraphicsDevice.Viewport = viewport;
        }
    }
}
