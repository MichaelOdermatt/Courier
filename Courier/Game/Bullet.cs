using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Bullet : Node
    {
        private readonly Sprite sprite;
        private readonly Vector2 bulletDirection;

        private readonly float speed = 500f;

        public Bullet(Node parent, Vector2 position, Vector2 direction) : base(parent)
        {
            sprite = new Sprite(this, "BulletSmall");
            Children.Add(sprite);

            this.LocalPosition = position;
            this.bulletDirection = direction;
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var velocity = bulletDirection * (speed * deltaTime);

            // Apply the velocity to the Bullet.
            LocalPosition += velocity;
        }
    }
}
