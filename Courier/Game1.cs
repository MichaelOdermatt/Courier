using Courier.Engine;
using Courier.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Courier
{
    // Sample Project: https://github.dev/MonoGame/MonoGame.Samples/blob/3.8.0/Platformer2D/Platformer2D.Core/Game/Level.cs#L518

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const int DefaultRealScreenWidth = 1920;
        private const int DefaultRealScreenHeight = 1080;

        private GraphicsDeviceManager graphics;
        /// <summary>
        /// SpriteBatch used for drawing world space sprites.
        /// </summary>
        private SpriteBatch worldSpaceSpriteBatch;
        /// <summary>
        /// SpriteBatch used for drawing screen space sprites.
        /// </summary>
        private SpriteBatch screenSpaceSpriteBatch;
        private AssetManager assetManager;
        private ResolutionIndependentRenderer renderer;
        private Camera2D camera;

        private Level1 level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            IsMouseVisible = true;

            renderer = new ResolutionIndependentRenderer(this);

            assetManager = new AssetManager(Services);

            // Camera is created outside the Scene since SpriteBatch.Begin and ResolutionINdependentRenderer need a reference to it.
            camera = new Camera2D(renderer);
            level = new Level1(camera);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            level.Initialize();
            InitRenderer();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";

            worldSpaceSpriteBatch = new SpriteBatch(GraphicsDevice);
            screenSpaceSpriteBatch = new SpriteBatch(GraphicsDevice);

            assetManager.LoadTextures();
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }

            // TODO: Add your update logic here
            level.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            // TODO only draw what is in view of the camera.

            // Note: For UI in the future, use a seperate sprite batch that isn't given the camera's transformation matrix.
            renderer.BeginDraw();

            worldSpaceSpriteBatch.Begin(
                SpriteSortMode.Deferred, 
                BlendState.AlphaBlend, 
                SamplerState.PointClamp, 
                DepthStencilState.None, 
                RasterizerState.CullNone, 
                null, 
                camera.GetViewTransformationMatrix()
            );
            screenSpaceSpriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                null
            );

            level.Draw(worldSpaceSpriteBatch, screenSpaceSpriteBatch, assetManager);

            worldSpaceSpriteBatch.End();
            screenSpaceSpriteBatch.End();

            base.Draw(gameTime);
        }

        private void InitRenderer()
        {
            graphics.PreferredBackBufferWidth = DefaultRealScreenWidth;
            graphics.PreferredBackBufferHeight = DefaultRealScreenHeight;
            graphics.ApplyChanges();

            // The virtual resolution we always want our game to display at.
            renderer.VirtualWidth = 1025;
            renderer.VirtualHeight = 512;
            renderer.ScreenWidth = DefaultRealScreenWidth;
            renderer.ScreenHeight = DefaultRealScreenHeight;
            renderer.Initialize();

            camera.RecalculateTransformationMatrices();
        }
    }
}
