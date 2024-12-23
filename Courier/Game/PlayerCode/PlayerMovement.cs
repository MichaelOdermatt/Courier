using Courier.Engine.Extensions;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerMovement
    {
        private readonly PlayerInput playerInput;
        private readonly PlayerFuel playerFuel;

        private const float RotateSpeed = 0.0266f;

        private Vector2 gravityDirection = Vector2.UnitY;

        private const float TerminalSpeed = 550f;
        private const float ThrustPower = 1.2f;
        private const float GravityPower = 5f;
        private const float LiftPower = 0.5f;
        private const float InducedDragPower = 0.3f;
        private const float DragPower = 0.8f;
        private const float ThrustFuelDepletionAmount = 9f;
        private const float NormalFuelDepletionAmount = 2f;

        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public float AngleOfAttack { get; private set; } = 0f;

        public PlayerMovement(PlayerInput playerInput, PlayerFuel playerFuel)
        {
            this.playerInput = playerInput;
            this.playerFuel = playerFuel;
        }

        /// <summary>
        /// Calculate and update the new movement values for this Update call.
        /// </summary>
        /// <param name="gameTime">The gameTime object.</param>
        /// <param name="velocity">The players old velocity.</param>
        /// <returns></returns>
        public void UpdateMovement(GameTime gameTime)
        {
            var isThrustPressed = playerInput.IsPlayerPressingAccelerate();

            // Rotate the velocity vector based on the Players input.
            var steeringDirection = playerInput.GetPlayerSteeringDirection();

            // Disable the Player's steering if they are out of fuel.
            if (playerFuel.IsOutOfFuel()) {
                steeringDirection = 0;
            }

            var steeringAmount = steeringDirection * RotateSpeed;

            var newVelocity = Velocity.Rotate(steeringAmount);

            var normalizedVelocity = newVelocity == Vector2.Zero ? Vector2.Zero : Vector2.Normalize(newVelocity);
            var angleOfAttack = CalcAngleOfAttack(newVelocity);
            var normalizedAngleOfAttack = NormalizeAngleOfAttack(angleOfAttack);

            var gravity = gravityDirection * GravityPower;
            var lift = CalcLift(normalizedAngleOfAttack, normalizedVelocity);
            var drag = CalcDrag(normalizedAngleOfAttack, normalizedVelocity);

            // Apply thrust if space is pressed.
            if (isThrustPressed && !playerFuel.IsOutOfFuel())
            {
                var thrust = normalizedVelocity * ThrustPower;
                newVelocity += thrust;
            }

            // Reduce the player's fuel level.
            if (!playerFuel.IsOutOfFuel()) {
                var fuelDepletionAmount = isThrustPressed ? ThrustFuelDepletionAmount : NormalFuelDepletionAmount;
                playerFuel.DepleteFuel(gameTime, fuelDepletionAmount);
            }

            // Apply gravity.
            newVelocity += gravity;

            // Apply lift.
            newVelocity += lift;

            // Apply Drag.
            newVelocity += drag;

            // Cap velocity to the terminal velocty value.
            if (newVelocity.Length() >= TerminalSpeed)
            {
                newVelocity = Vector2.Normalize(newVelocity) * TerminalSpeed;
            }

            Velocity = newVelocity;
            AngleOfAttack = angleOfAttack;
        }

        /// <summary>
        /// Calculate and return the Players angle of attack.
        /// </summary>
        private float CalcAngleOfAttack(Vector2 velocity)
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
            var liftForce = liftDirection * (liftVelocity.Length() * LiftPower * liftCoefficient);

            // Calculate induced drag.
            var dragDirection = -normalizedVelocity;
            var inducedDragForce = dragDirection * (liftVelocity.Length() * InducedDragPower * MathF.Pow(liftCoefficient, 2));

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
            return dragDirection * (velocity.Length() * dragCoefficient * DragPower);
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
    }
}
