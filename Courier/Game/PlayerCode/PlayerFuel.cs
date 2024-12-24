using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courier.Game.EventData;
using Courier.Engine.PubSubCustom;

namespace Courier.Game.PlayerCode
{
    public class PlayerFuel
    {
        private readonly Hub hub;

        private const float MaxFuelAmount = 100f;

        private float fuelAmount = 50f;

        /// <summary>
        /// The fuel amount but scaled to a value between 0 and 1.
        /// </summary>
        public float FuelAmountScaled { get => fuelAmount / MaxFuelAmount; }

        public PlayerFuel(Hub hub)
        {
            this.hub = hub;
        }


        /// <summary>
        /// Updates the players fuel amount by the given amount.
        /// </summary>
        /// <param name="amountToAdd">The amount the increase the fuel level by.</param>
        public void IncreaseFuelAmount(float amountToAdd)
        {
            var newFuelAmount = fuelAmount + amountToAdd >= MaxFuelAmount ? MaxFuelAmount : fuelAmount + amountToAdd;
            fuelAmount = newFuelAmount;
            hub.Publish(new UpdateFuelEvent
            {
                NewFuelLevel = FuelAmountScaled,
            });
        }

        /// <summary>
        /// Depletes the fuel amount by the given fuelDepletionAmount.
        /// </summary>
        /// <param name="gameTime">The Update method GameTime object.</param>
        /// <param name="fuelDepletionAmount">The amount be depleted.</param>
        public void DepleteFuel(GameTime gameTime, float fuelDepletionAmount)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fuelAmount -= fuelDepletionAmount * deltaTime;
            hub.Publish(new UpdateFuelEvent
            {
                NewFuelLevel = FuelAmountScaled,
            });
        }

        public bool IsOutOfFuel()
        {
            return fuelAmount <= 0; 
        }
    }
}
