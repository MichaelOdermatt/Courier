﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerFuel
    {
        private const float MaxFuelAmount = 100f;
        private const float FuelDepletionAmount = 10f;

        private float fuelAmount = 100f;

        /// <summary>
        /// The fuel amount but scaled to a value between 0 and 1.
        /// </summary>
        public float FuelAmountScaled { get => fuelAmount / MaxFuelAmount; }

        /// <summary>
        /// Depletes the fuel amount by 1 increment of the FuelDepletionAmount.
        /// </summary>
        /// <param name="gameTime">The Update method GameTime object.</param>
        public void DepleteFuel(GameTime gameTime)
        {
            var deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            fuelAmount -= FuelDepletionAmount * deltaTime;
        }

        public bool IsOutOfFuel()
        {
            return fuelAmount <= 0; 
        }
    }
}