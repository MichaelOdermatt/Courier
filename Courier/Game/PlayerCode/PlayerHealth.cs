﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerHealth
    {
        /// <summary>
        /// The current Player health value.
        /// </summary>
        public float Health { get; set; } = 2;

        /// <summary>
        /// Boolean value representing if the player's health is zero or less.
        /// </summary>
        public bool HasZeroHealth { get =>  Health <= 0; }

        public void reduceHealth(float reductionAmount)
        {
            Health -= reductionAmount;
        }
    }
}