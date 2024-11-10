using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;

namespace Courier.Game
{
    public class SkyBackground : Node
    {
        private readonly Sprite sprite;

        public SkyBackground(Node parent) : base(parent)
        {
            var sprite = new Sprite(this, "Sky");
            sprite.IsWorldSpaceSprite = false;
            sprite.Origin = Vector2.Zero;
            Children.Add(sprite);
        }
    }
}