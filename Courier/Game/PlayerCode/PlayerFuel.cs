using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PubSub;
using Courier.Game.EventData;

namespace Courier.Game.PlayerCode
{
    public class PlayerFuel
    {
        private readonly Hub hub;

        private const float MaxFuelAmount = 100f;
        private const float FuelDepletionAmount = 30f;

        private float fuelAmount = 100f;

        /// <summary>
        /// The fuel amount but scaled to a value between 0 and 1.
        /// </summary>
        public float FuelAmountScaled { get => fuelAmount / MaxFuelAmount; }

        public PlayerFuel(Hub hub)
        {
            this.hub = hub;
        }

        /// <summary>
        /// Adds the given amount of fuel to the players fuel amount.
        /// </summary>
        public void AddFuel(float newFuelAmount)
        {
            var updatedFuelAmount = fuelAmount + newFuelAmount;
            fuelAmount = updatedFuelAmount >= MaxFuelAmount ? MaxFuelAmount : updatedFuelAmount;
            hub.Publish(new UpdateFuelEvent
            {
                newFuelLevel = FuelAmountScaled,
            });
        }

        /// <summary>
        /// Depletes the fuel amount by 1 increment of the FuelDepletionAmount.
        /// </summary>
        /// <param name="gameTime">The Update method GameTime object.</param>
        public void DepleteFuel(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fuelAmount -= FuelDepletionAmount * deltaTime;
            hub.Publish(new UpdateFuelEvent
            {
                newFuelLevel = FuelAmountScaled,
            });
        }

        public bool IsOutOfFuel()
        {
            return fuelAmount <= 0; 
        }
    }
}
