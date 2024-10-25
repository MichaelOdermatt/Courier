using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Extensions;
using Courier.Engine.Nodes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class Player : PlayerController
    {
        private readonly Sprite sprite;
        /// <summary>
        /// A Node which has the Players position with an additional offset. This is what Enemies should fire at.
        /// </summary>
        private readonly Node enemyTarget;
        private readonly Camera2D camera;
        private readonly PlayerMovement playerMovement = new PlayerMovement();

        private float maxEnemyTargetDistance = 500f;
        private float speedAtMaxEnemyTargetDistance = 12f;

        /// <summary>
        /// The GlobalPosition of the EnemyTarget object.
        /// </summary>
        public Vector2 EnemyTargetGlobalPosition { get => enemyTarget.GlobalPosition; }

        public Player(Node parent, ICollisionShape collisionShape, Camera2D camera) : base(parent, collisionShape)
        {
            sprite = new Sprite(this, "Player");
            Children.Add(sprite);
            enemyTarget = new Node(this);
            Children.Add(enemyTarget);

            this.camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Velocity = playerMovement.CalcNewVelocity(gameTime, Velocity);
            ApplyVelocity();

            // TODO lerp the rotation, so it looks smoother? Or would that make it look strange
            // Update the sprites rotation to match the angle of attack.
            var angleOfAttack = playerMovement.CalcAngleOfAttack(Velocity);
            sprite.Rotation = angleOfAttack;

            UpdateEnemyTargetPosition();

            // Update the camera position to always follow the Player.
            camera.SetPosition(GlobalPosition);
        }

        public override void OnCollision(ICollisionNode collisionNode)
        {
            // TODO implementation
        }

        /// <summary>
        /// Update the Enemy Target Position.
        /// </summary>
        private void UpdateEnemyTargetPosition()
        {
            var distanceFromPlayer = Velocity.Length() / speedAtMaxEnemyTargetDistance * maxEnemyTargetDistance;

            distanceFromPlayer = Math.Clamp(distanceFromPlayer, 0, maxEnemyTargetDistance);
            var playerXDirectionSign = MathF.Sign(Velocity.X);
            enemyTarget.LocalPosition = new Vector2(distanceFromPlayer * playerXDirectionSign, 0);
        }
    }
}
