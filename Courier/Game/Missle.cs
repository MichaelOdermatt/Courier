using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Missle : Node
    {
        private const float MissleAcceleration = 15;
        private const float TerminalMissleSpeed = 500;

        private readonly Sprite sprite;

        private readonly Player player;

        private readonly CollisionNode collisionNode;
        private const CollisionNodeType CollisionType = CollisionNodeType.Enemy;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.Player };

        private Vector2 Velocity = Vector2.Zero;

        public Missle(Node parent, Player player) : base(parent)
        {
            this.player = player;
            sprite = new Sprite(this, "Missle");
            Children.Add(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            Velocity += Vector2.Normalize(vectorToPlayer) * MissleAcceleration;
            // Cap the missles velocity.
            if (Velocity.Length() >= TerminalMissleSpeed)
            {
                Velocity = TerminalMissleSpeed * Vector2.Normalize(Velocity);
            }

            // Rotate the sprite to face the Player. The sprite rotation will be updated in the next Draw call.
            var normalizedVelocity = Vector2.Normalize(Velocity);
            var angleToPoint = MathF.Atan2(normalizedVelocity.Y, normalizedVelocity.X);
            sprite.Rotation = angleToPoint;

            // Apply the velocity to the position.
            LocalPosition += Velocity * deltaTime;
        }
    }
}
