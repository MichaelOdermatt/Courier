using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Engine
{
    // TODO comment this class
    public class GameTimer
    {
        private readonly float duration;
        private readonly Action action;

        private double currentTime = 0.0;

        private bool isRunning = false;

        public bool Loop { get; set; }

        public GameTimer(float duration, Action action) 
        {
            this.duration = duration;
            this.action = action;
        }

        public void Start()
        {
            isRunning = true; 
        }

        public void Tick(GameTime gameTime)
        {
            if (!isRunning)
            {
                return;
            }

            currentTime += gameTime.ElapsedGameTime.TotalSeconds; 
            if (currentTime >= duration)
            {
                currentTime = 0.0;
                action.Invoke();
                if (!Loop)
                {
                    isRunning = false;
                }
            }
        }
    }
}
