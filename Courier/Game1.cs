using Courier.Engine;
using Courier.Engine.Render;
using Courier.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Courier
{
    // Sample Project: https://github.dev/MonoGame/MonoGame.Samples/blob/3.8.0/Platformer2D/Platformer2D.Core/Game/Level.cs#L518

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private SpriteBatch spriteBatch;
        private AssetManager assetManager;
        private ResolutionIndependentRenderer renderer;
        private Camera2D camera;
        private SpriteRenderer spriteRenderer;

        private Level1 level;

        public Game1()
        {
            IsMouseVisible = true;
            renderer = new ResolutionIndependentRenderer(this);
            assetManager = new AssetManager(Services);
            camera = new Camera2D(renderer);

            level = new Level1(camera);
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            level.Initialize();
            renderer.Initialize();
            camera.RecalculateTransformationMatrices();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteRenderer = new SpriteRenderer(spriteBatch, assetManager, camera);

            assetManager.LoadTextures();
            assetManager.LoadFonts();
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
            spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone,
                null,
                renderer.GetTransformationMatrix()
            );

            renderer.BeginDraw();

            level.Draw(spriteRenderer);
            spriteRenderer.Render();

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
