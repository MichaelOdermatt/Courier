using Courier.Engine;
using Courier.Engine.Collisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Player : PlayerController
    {
        private readonly Sprite sprite;
        private readonly Camera2D camera;

        private float rotateSpeed = 1.5f;
        // TODO apply rotation limits.
        private int maxRotation = 90;
        private int minRotation = -90;

        private float gravity = 5f;
        private Vector2 gravityDirection = Vector2.UnitY;

        public Player(Node parent, ICollisionShape collisionShape, Camera2D camera) : base(parent, collisionShape)
        {
            sprite = new Sprite(this, "ball");
            Children.Add(sprite);
            this.camera = camera;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            UpdateVelocity(gameTime);
            ApplyVelocity();

            // Update the camera position to always follow the Player.
            camera.SetPosition(GlobalPosition);
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;

            // Rotate the velocity vector based on the Players input.
            var steeringDirection = GetPlayerSteeringDirection();
            var rotationMatrix = Matrix.CreateRotationZ(steeringDirection * rotateSpeed * deltaTime);
            Velocity = Vector2.Transform(Velocity, rotationMatrix);

            // Apply gravity.
            Velocity += gravityDirection * gravity * deltaTime;
        }

        /// <summary>
        /// Reads the Players input and returns their steering direction. 1 if they are steering up, -1 if they are steer down, and 0 if there is no player input.
        /// </summary>
        private int GetPlayerSteeringDirection()
        {
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.S))
            {
                return -1;
            }

            if (keyState.IsKeyDown(Keys.W))
            {
                return 1;
            }

            return 0;
        }

        public override void OnCollision(ICollisionNode collisionNode)
        {
            // TODO implementation
        }
    }
}
