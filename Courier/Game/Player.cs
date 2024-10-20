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

        private Vector2 gravityDirection = Vector2.UnitY;

        private float thrustPower = 100f;
        private float gravityPower = 5f;
        private float liftPower = 0.1f;
        private float inducedDragPower = 0.04f;
        private float dragPower = 0.1f;

        public Player(Node parent, ICollisionShape collisionShape, Camera2D camera) : base(parent, collisionShape)
        {
            sprite = new Sprite(this, "Player");
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

        public override void OnCollision(ICollisionNode collisionNode)
        {
            // TODO implementation
        }

        private void UpdateVelocity(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyState = Keyboard.GetState();

            var isSpacePressed = keyState.IsKeyDown(Keys.Space);

            // Rotate the velocity vector based on the Players input.
            var steeringDirection = GetPlayerSteeringDirection(keyState);
            var steeringAmount = steeringDirection * rotateSpeed * deltaTime;

            Velocity = Velocity.Rotate(steeringAmount);

            var normalizedVelocity = Velocity == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(Velocity);
            var angleOfAttack = CalcAngleOfAttack();

            var gravity = gravityDirection * gravityPower;
            var lift = CalcLift(angleOfAttack, normalizedVelocity);
            var drag = CalcDrag(angleOfAttack, normalizedVelocity);

            // Apply thrust if space is pressed.
            if (isSpacePressed)
            {
                var thrust = normalizedVelocity * (thrustPower * deltaTime);
                Velocity += thrust * deltaTime;
            }

            // Apply gravity.
            Velocity += gravity * deltaTime;

            // Apply lift.
            Velocity += lift * deltaTime;

            // Apply Drag.
            Velocity += lift * deltaTime;

            // Update the sprites rotation to match the angle of attack.
            sprite.Rotation = angleOfAttack;
        }

        /// <summary>
        /// Calculate and return the Players angle of attack.
        /// </summary>
        private float CalcAngleOfAttack()
        {
            return MathF.Atan2(Velocity.Y, Velocity.X);
        }

        /// <summary>
        /// Calculate and return the lift force and direction as a Vector2.
        /// </summary>
        /// <param name="angleOfAttack">The Players angle of attack.</param>
        /// <param name="normalizedVelocity">The Players normalized velocity.</param>
        private Vector2 CalcLift(float angleOfAttack, Vector2 normalizedVelocity)
        {
            var liftCoefficient = CalcLiftCoefficient(angleOfAttack);

            // Calculate lift.
            var liftDirection = new Vector2(normalizedVelocity.Y, normalizedVelocity.X);
            var liftVelocity = Velocity.Project(Vector2.UnitX);
            var liftForce = liftDirection * (liftVelocity.Length() * liftPower * liftCoefficient);

            // Calculate induced drag.
            var dragDirection = -normalizedVelocity;
            var inducedDragForce = dragDirection * (liftVelocity.Length() * inducedDragPower * MathF.Pow(liftCoefficient, 2));

            return inducedDragForce + liftForce;
        }

        /// <summary>
        /// Calculate and return the drag force and direction as a Vector2.
        /// </summary>
        /// <param name="angleOfAttack">The Players angle of attack.</param>
        /// <param name="normalizedVelocity">The Players normalized velocity.</param>
        private Vector2 CalcDrag(float angleOfAttack, Vector2 normalizedVelocity)
        {
            var dragCoefficient = CalcLiftCoefficient(angleOfAttack);
            var dragDirection = -normalizedVelocity;
            return dragDirection * (Velocity.Length() * dragCoefficient * dragPower);
        }

        /// <summary>
        /// Calculate and return the lift coefficient at the given angle of attack.
        /// </summary>
        private float CalcLiftCoefficient(float angleOfAttack)
        {
            return MathF.Sin(2 * angleOfAttack);
        }

        /// <summary>
        /// Calculate and return the drag coefficient at the given angle of attack.
        /// </summary>
        private float CalcDragCoefficient(float angleOfAttack)
        {
            return 0.02f + 1.0f * MathF.Pow(angleOfAttack, 2);
        }

        /// <summary>
        /// Reads the Players input and returns their steering direction. 1 if they are steering up, -1 if they are steer down, and 0 if there is no player input.
        /// </summary>
        /// <param name="keyState">The current keyboard state.</param>
        private int GetPlayerSteeringDirection(KeyboardState keyState)
        {
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
    }
}
