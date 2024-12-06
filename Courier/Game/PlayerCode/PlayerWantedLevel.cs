using Courier.Game.EventData;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.PlayerCode
{
    public class PlayerWantedLevel
    {
        private readonly Hub hub;

        private const int MaxWantedLevel = 5;
        private int currentWantedLevel = 0;

        public PlayerWantedLevel(Hub hub)
        {
            this.hub = hub;
            hub.Subscribe<CharcterHitEvent>(OnTownDestroyed);
        }

        /// <summary>
        /// Increases the player's wanted level by a set amount.
        /// </summary>
        private void IncreasePlayerWantedLevel()
        {
            currentWantedLevel += 1;
            hub.Publish(new UpdateWantedLevelEvent { NewWantedLevel = currentWantedLevel });
        }

        private void OnTownDestroyed(CharcterHitEvent eventData)
        {
            IncreasePlayerWantedLevel();
        }
    }
}
