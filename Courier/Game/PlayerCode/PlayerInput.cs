﻿using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerInput
    {
        private KeyboardState previousKeyboardState;
        private KeyboardState currentKeyboardState;

        /// <summary>
        /// Refreshes the keyboardState variable.
        /// </summary>
        public void UpdateKeyboardState()
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }

        /// <summary>
        /// Reads the Players input and returns their steering direction. 1 if they are steering up, -1 if they are steer down, and 0 if there is no player input.
        /// </summary>
        public int GetPlayerSteeringDirection()
        {
            if (currentKeyboardState.IsKeyDown(Keys.S))
            {
                return -1;
            }

            if (currentKeyboardState.IsKeyDown(Keys.W))
            {
                return 1;
            }

            return 0;
        }

        /// <summary>
        /// Reads the Players input and returns true if the player is pressing the accelerate button.
        /// </summary>
        public bool IsPlayerPressingAccelerate()
        {
            return currentKeyboardState.IsKeyDown(Keys.Space);
        }

        /// <summary>
        /// Reads the Players input and returns true if the player pressed the deliver package button.
        /// </summary>
        public bool HasPlayerPressedDeliverPackageKey()
        {
            var deliverPackageKey = Keys.Enter;
            return currentKeyboardState.IsKeyDown(deliverPackageKey) && !previousKeyboardState.IsKeyDown(deliverPackageKey);
        }
    }
}