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
        /// Boolean value used to track if an event was sent notifying all subscribers that a particular town was destroyed.
        /// </summary>
        private readonly bool[] hasSentDestroyedEvent;

        public TownManager(Node parent, List<Town> towns) : base(parent)
        {
            this.towns = towns.ToArray();
            Children.AddRange(towns);

            hasSentDestroyedEvent = new bool[this.towns.Length];
            for (int i = 0; i < this.towns.Length; i++)
            {
                hasSentDestroyedEvent[i] = false;
            }

            hub = Hub.Default;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PublishAnyTownDestroyedEvents();
        }

        /// <summary>
        /// Checks all towns and publishes an event for each town that was destroyed and hasn't had an event published yet.
        /// </summary>
        private void PublishAnyTownDestroyedEvents()
        {
            for (int i = 0; i < this.towns.Length; i++)
            {
                var town = this.towns[i];
                if (town.Destroyed && !hasSentDestroyedEvent[i])
                {
                    hub.Publish(new TownDestroyedEvent());
                    hasSentDestroyedEvent[i] = true;
                }
            }
        }
    }
}
