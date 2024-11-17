using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Parcel : Node
    {
        private readonly Sprite sprite;
        private readonly Vector2 parcelDirection = Vector2.UnitY;

        private const float Speed = 4;

        private Vector2 velocity = Vector2.Zero;

        public Parcel(Node parent) : base(parent)
        {
            sprite = new Sprite(this, "Parcel");
            Children.Add(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            velocity += parcelDirection * (Speed * deltaTime);

            // Apply the velocity to the Bullet.
            LocalPosition += velocity;
        }
    }
}
