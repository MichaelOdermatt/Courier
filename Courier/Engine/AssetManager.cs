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
        private const string FontsDirectory = "Fonts/";
        private const string SpritesDirectory = "Sprites/";

        private readonly ContentManager contentManager;

        /// <summary>
        /// Dictionary to store all Texture2D assets.
        /// </summary>
        public Dictionary<string, Texture2D> Textures { get; set; }

        /// <summary>
        /// Dictionary to store all Font assets.
        /// </summary>
        public Dictionary<string, SpriteFont> Fonts { get; set; }

        public AssetManager(IServiceProvider serviceProvider)
        {
            contentManager = new ContentManager(serviceProvider, "Content");
            Textures = new Dictionary<string, Texture2D>();
            Fonts = new Dictionary<string, SpriteFont>();
        }

        /// <summary>
        /// Populates the Textures Dictionary with all the Texture2D assets.
        /// </summary>
        public void LoadTextures()
        {
            Textures.Add("Player", contentManager.Load<Texture2D>($"{SpritesDirectory}Player"));
            Textures.Add("FuelMeterFill", contentManager.Load<Texture2D>($"{SpritesDirectory}FuelMeterFill"));
            Textures.Add("FuelMeterOutline", contentManager.Load<Texture2D>($"{SpritesDirectory}FuelMeterOutline"));
            Textures.Add("Gunner", contentManager.Load<Texture2D>($"{SpritesDirectory}Gunner"));
            Textures.Add("Tank", contentManager.Load<Texture2D>($"{SpritesDirectory}Tank"));
            Textures.Add("BulletSmall", contentManager.Load<Texture2D>($"{SpritesDirectory}BulletSmall"));
            Textures.Add("BulletLarge", contentManager.Load<Texture2D>($"{SpritesDirectory}BulletLarge"));
            Textures.Add("LineTexture", contentManager.Load<Texture2D>($"{SpritesDirectory}LineTexture"));
            Textures.Add("LineTextureWhite", contentManager.Load<Texture2D>($"{SpritesDirectory}LineTextureWhite"));
            Textures.Add("Town", contentManager.Load<Texture2D>($"{SpritesDirectory}Town"));
            Textures.Add("TownCelebrate", contentManager.Load<Texture2D>($"{SpritesDirectory}TownCelebrate"));
            Textures.Add("Sky", contentManager.Load<Texture2D>($"{SpritesDirectory}Sky"));
            Textures.Add("Parcel", contentManager.Load<Texture2D>($"{SpritesDirectory}Parcel"));
            Textures.Add("Missile", contentManager.Load<Texture2D>($"{SpritesDirectory}Missile"));
        }

        /// <summary>
        /// Populates the Fonts Dictionary with all the SpriteFont assets.
        /// </summary>
        public void LoadFonts()
        {
            Fonts.Add("GameplayFont", contentManager.Load<SpriteFont>($"{FontsDirectory}GameplayFont"));
        }
    }
}
