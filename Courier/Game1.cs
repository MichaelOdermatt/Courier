using Courier.Content;
using Courier.Game;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Courier
{
    // Sample Project: https://github.dev/MonoGame/MonoGame.Samples/blob/3.8.0/Platformer2D/Platformer2D.Core/Game/Level.cs#L518

    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private AssetManager assetManager;

        private Level level;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            assetManager = new AssetManager(Services);
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            level = new Level();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            Content.RootDirectory = "Content";

            spriteBatch = new SpriteBatch(GraphicsDevice);

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
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            level.Draw(spriteBatch, assetManager);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
