using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Render
{
    public class SpriteRenderer
    {
        private readonly SpriteBatch spriteBatch;
        private readonly AssetManager assetManager;
        private readonly Camera2D camera;

        private readonly List<SpriteRenderData> spritesToRender = new List<SpriteRenderData>();

        public SpriteRenderer(SpriteBatch spriteBatch, AssetManager assetManager, Camera2D camera)
        {
            this.spriteBatch = spriteBatch;
            this.assetManager = assetManager;
            this.camera = camera;
        }

        /// <summary>
        /// Adds the given SpriteRenderData object to the list of Sprites that need to be rendered.
        /// </summary>
        public void AddSprite(SpriteRenderData spriteRenderData)
        {
            spritesToRender.Add(spriteRenderData);
        }

        /// <summary>
        /// Draws all the Sprites for all the added SpriteRenderData then clears the list of sprites to render.
        /// </summary>
        public void Render()
        {
            foreach (var data in spritesToRender)
            {
                // If its in world space and the sprite is not in the camera's view, don't draw it. Or if the Sprite's Visible property is false, don't draw it.
                if (!data.NeverCull && data.IsWorldSpaceSprite && !camera.IsPointInCameraView(data.Position))
                {
                    continue;
                }

                // If the sprite is not in world space then apply the camera's transform to it.
                if (data.IsWorldSpaceSprite)
                {
                    data.Position = Vector2.Transform(data.Position, camera.GetViewTransformationMatrix());
                }

                var texture = assetManager.Textures[data.TextureKey];
                var origin = data.Origin ?? new Vector2(texture.Width / 2, texture.Height / 2);

                spriteBatch.Draw(texture, data.Position, null, Color.White, data.Rotation, origin, data.Scale, SpriteEffects.None, data.LayerDepth);
            }

            spritesToRender.Clear();
        }
    }
}
