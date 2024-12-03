using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Courier.Game.TownCode
{
    public class TownManager : Node
    {
        private readonly Hub hub; 
        private readonly Town[] towns;
        /// <summary>
        /// Boolean value used to track if an event was sent notifying all subscribers that a particular town was hit.
        /// </summary>
        private readonly bool[] hasSentHitEvent;

        public TownManager(Node parent, List<Town> towns) : base(parent)
        {
            this.towns = towns.ToArray();
            Children.AddRange(towns);

            hasSentHitEvent = new bool[this.towns.Length];
            for (int i = 0; i < this.towns.Length; i++)
            {
                hasSentHitEvent[i] = false;
            }

            hub = Hub.Default;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PublishAnyTownHitEvents();
        }

        /// <summary>
        /// Checks all towns and publishes an event for each town that was hit and hasn't had an event published yet.
        /// </summary>
        private void PublishAnyTownHitEvents()
        {
            for (int i = 0; i < this.towns.Length; i++)
            {
                var town = this.towns[i];
                if (town.Destroyed && !hasSentHitEvent[i])
                {
                    hub.Publish(new CharcterHitEvent());
                    hasSentHitEvent[i] = true;
                }
            }
        }
    }
}
