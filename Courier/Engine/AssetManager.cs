using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    public class AssetManager
    {
        private const string spritesDirectory = "Sprites/";

        private ContentManager contentManager;

        /// <summary>
        /// Dictionary to store all Texture2D assets.
        /// </summary>
        public Dictionary<string, Texture2D> Textures { get; set; }

        public AssetManager(IServiceProvider serviceProvider)
        {
            contentManager = new ContentManager(serviceProvider, "Content");
            Textures = new Dictionary<string, Texture2D>();
        }

        /// <summary>
        /// Populates the Textures Dictionary with all the Texture2D assets.
        /// </summary>
        public void LoadTextures()
        {
            Textures.Add("Player", contentManager.Load<Texture2D>($"{spritesDirectory}Player"));
            Textures.Add("FuelMeterFill", contentManager.Load<Texture2D>($"{spritesDirectory}FuelMeterFill"));
            Textures.Add("FuelMeterOutline", contentManager.Load<Texture2D>($"{spritesDirectory}FuelMeterOutline"));
            Textures.Add("Gunner", contentManager.Load<Texture2D>($"{spritesDirectory}Gunner"));
            Textures.Add("BulletSmall", contentManager.Load<Texture2D>($"{spritesDirectory}BulletSmall"));
            Textures.Add("BulletLarge", contentManager.Load<Texture2D>($"{spritesDirectory}BulletLarge"));
            Textures.Add("LineTexture", contentManager.Load<Texture2D>($"{spritesDirectory}LineTexture"));
        }
    }
}
