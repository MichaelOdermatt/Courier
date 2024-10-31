using Courier.Engine.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerMovement
    {
        private float rotateSpeed = 1.5f;

        private Vector2 gravityDirection = Vector2.UnitY;

        private float terminalVelocity = 12f;
        private float thrustPower = 100f;
        private float gravityPower = 5f;
        private float liftPower = 0.1f;
        private float inducedDragPower = 0.1f;
        private float dragPower = 0.09f;

        private float maxFuelAmount = 100f;
        private float fuelAmount = 100f;
        private float fuelDepletionAmount = 0.5f;

        /// <summary>
        /// The fuel amount but scaled to a value between 0 and 1.
        /// </summary>
        public float FuelAmountScaled { get => fuelAmount / maxFuelAmount; }

        /// <summary>
        /// Calculate and return the new velocity value for the Update.
        /// </summary>
        /// <param name="gameTime">The gameTime object.</param>
        /// <param name="velocity">The players old velocity.</param>
        /// <returns></returns>
        public Vector2 CalcNewVelocity(GameTime gameTime, Vector2 velocity)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var keyState = Keyboard.GetState();

            var isSpacePressed = keyState.IsKeyDown(Keys.Space);

            // Rotate the velocity vector based on the Players input.
            var steeringDirection = GetPlayerSteeringDirection(keyState);
            var steeringAmount = steeringDirection * rotateSpeed * deltaTime;

            velocity = velocity.Rotate(steeringAmount);

            var normalizedVelocity = velocity == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(velocity);
            var angleOfAttack = CalcAngleOfAttack(velocity);
            var normalizedAngleOfAttack = NormalizeAngleOfAttack(angleOfAttack);

            var gravity = gravityDirection * gravityPower;
            var lift = CalcLift(normalizedAngleOfAttack, normalizedVelocity);
            var drag = CalcDrag(normalizedAngleOfAttack, normalizedVelocity);

            // Apply thrust if space is pressed.
            if (isSpacePressed && fuelAmount > 0)
            {
                fuelAmount -= fuelDepletionAmount;
                var thrust = normalizedVelocity * (thrustPower * deltaTime);
                velocity += thrust * deltaTime;
            }

            // Apply gravity.
            velocity += gravity * deltaTime;

            // Apply lift.
            velocity += lift * deltaTime;

            // Apply Drag.
            velocity += drag * deltaTime;

            // Cap velocity to the terminal velocty value.
            if (velocity.Length() >= terminalVelocity)
            {
                velocity = Vector2.Normalize(velocity) * terminalVelocity;
            }

            return velocity;
        }

        /// <summary>
        /// Calculate and return the Players angle of attack.
        /// </summary>
        public float CalcAngleOfAttack(Vector2 velocity)
        {
            return MathF.Atan2(velocity.Y, velocity.X);
        }

        /// <summary>
        /// Returns the angle of attack normalized so that it's always a value between 1/2 PI and -1/2 PI. This is useful 
        /// when calculating drag and lift, as it allows us to get the same value reguardless of whether the plane is traveling to the left or right.
        /// </summary>
        /// <param name="angle">The angle of attack value to normalize.</param>
        private float NormalizeAngleOfAttack(float angle)
        {
            if (angle <= -MathF.PI * 0.5f)
            {
                angle += MathF.PI;
            }
            if (angle >= MathF.PI * 0.5f)
            {
                angle -= MathF.PI;
            }
            return angle;
        }

        /// <summary>
        /// Calculate and return the lift force and direction as a Vector2.
        /// </summary>
        /// <param name="angleOfAttack">The Players angle of attack.</param>
        /// <param name="velocity">The Players velocity.</param>
        private Vector2 CalcLift(float angleOfAttack, Vector2 velocity)
        {
            var normalizedVelocity = velocity == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(velocity);
            var liftCoefficient = CalcLiftCoefficient(angleOfAttack);

            // Calculate lift.
            var liftDirection = new Vector2(normalizedVelocity.Y, normalizedVelocity.X);
            var liftVelocity = velocity.Project(Vector2.UnitX);
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
        /// <param name="velocity">The Players velocity.</param>
        private Vector2 CalcDrag(float angleOfAttack, Vector2 velocity)
        {
            var normalizedVelocity = velocity == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(velocity);
            var dragCoefficient = CalcDragCoefficient(angleOfAttack);
            var dragDirection = -normalizedVelocity;
            return dragDirection * (velocity.Length() * dragCoefficient * dragPower);
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
