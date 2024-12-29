using Courier.Engine.Collisions;
using Courier.Engine.Collisions.CollisionShapes;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.MissileCode
{
    public class Missile : Node
    {
        private const float MissileAcceleration = 20;
        private const float TerminalMissileSpeed = 600;

        private readonly Sprite sprite;

        private readonly Player player;

        private readonly CollisionNode collisionNode;
        private const CollisionNodeType collisionType = CollisionNodeType.Missile;
        private readonly CollisionNodeType[] collisionTypeMask = { CollisionNodeType.Player, CollisionNodeType.Ground };

        private Vector2 Velocity = Vector2.Zero;

        /// <summary>
        /// Bool value used to mark whether this Missile should be destroyed or not.
        /// </summary>
        public bool ShouldDestroy { get; private set; } = false;

        public Missile(Node parent, Player player) : base(parent)
        {
            this.player = player;
            sprite = new Sprite(this, "Missile");
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, 7f);
            this.collisionNode = new CollisionNode(this, collisionShape, collisionType, collisionTypeMask);
            collisionNode.OnCollision += OnCollide;
            Children.Add(collisionNode);
        }

        public override void Update(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            Velocity += Vector2.Normalize(vectorToPlayer) * MissileAcceleration;
            // Cap the missiles velocity.
            if (Velocity.Length() >= TerminalMissileSpeed)
            {
                Velocity = TerminalMissileSpeed * Vector2.Normalize(Velocity);
            }

            // Rotate the sprite to face the Player. The sprite rotation will be updated in the next Draw call.
            var normalizedVelocity = Vector2.Normalize(Velocity);
            var angleToPoint = MathF.Atan2(normalizedVelocity.Y, normalizedVelocity.X);
            sprite.Rotation = angleToPoint;

            // Apply the velocity to the position.
            LocalPosition += Velocity * deltaTime;
        }

        private void OnCollide(object sender, CollisionEventArgs eventArgs)
        {
            ShouldDestroy = true;
        }
    }
}
