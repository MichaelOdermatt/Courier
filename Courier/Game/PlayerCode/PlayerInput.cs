using Microsoft.Xna.Framework.Input;
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
        /// Reads the Players input and returns true if the Player is pressing the accelerate button.
        /// </summary>
        public bool IsPlayerPressingAccelerate()
        {
            return currentKeyboardState.IsKeyDown(Keys.Space);
        }

        /// <summary>
        /// Reads the Players input and returns true if the Player is pressing the deliver parcel button.
        /// </summary>
        public bool IsPlayerPressingDeliverParcelKey()
        {
            return currentKeyboardState.IsKeyDown(Keys.Enter);
        }

        /// <summary>
        /// Reads the Players input and returns true if the Player pressed the quick restart button.
        /// </summary>
        public bool HasPlayerPressedQuickRestartKey()
        {
            return HasPlayerPressedKey(Keys.R);
        }

        /// <summary>
        /// Compares the Players current and previous keyboard states to check if the given key was pressed a single time.
        /// </summary>
        private bool HasPlayerPressedKey(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key) && !previousKeyboardState.IsKeyDown(key);
        }
    }
}
