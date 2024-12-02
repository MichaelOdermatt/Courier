using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    public class GameTimer
    {
        private readonly Action action;

        private double currentTime = 0.0;

        /// <summary>
        /// The amount of time it take for the timer to complete.
        /// </summary>
        public float Duration { get; set; }

        /// <summary>
        /// Bool keeps track of if the timer is currently running.
        /// </summary>
        public bool IsRunning { get; private set; } = false;

        /// <summary>
        /// If true the timer will restart once the set duration has elapsed.
        /// </summary>
        public bool Loop { get; set; }

        /// <summary>
        /// GameTimer which can be used to run code after a set duration of time.
        /// </summary>
        /// <param name="duration">The amount of time that should pass before the Action is invoked.</param>
        /// <param name="action">The Action to invoke once the duration has elapsed.</param>
        public GameTimer(float duration, Action action) 
        {
            Duration = duration;
            this.action = action;
        }

        /// <summary>
        /// Starts the GameTimer.
        /// </summary>
        public void Start()
        {
            IsRunning = true; 
        }

        /// <summary>
        /// Call this function in Update. Calling this function lets the timer update and check if the set duration has elapsed.
        /// </summary>
        public void Tick(GameTime gameTime)
        {
            if (!IsRunning)
            {
                return;
            }

            currentTime += gameTime.ElapsedGameTime.TotalSeconds; 
            if (currentTime >= Duration)
            {
                currentTime = 0.0;
                action.Invoke();
                if (!Loop)
                {
                    IsRunning = false;
                }
            }
        }
    }
}
