using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine.Render
{
    public class SpriteRenderData
    {
        public string TextureKey { get; set; }
        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2? Origin { get; set; }
        public Vector2 Scale { get; set; }
        public float LayerDepth { get; set; }
        public bool IsWorldSpaceSprite { get; set; }
        /// <summary>
        /// Normally Sprites aren't added to the sprite batch if they are in world space and outside the view of the camera.
        /// However, if this value is set to true, Sprites won't be culled not matter what, even if they are outside the camera view.
        /// </summary>
        public bool NeverCull { get; set; } = false;
    }
}
