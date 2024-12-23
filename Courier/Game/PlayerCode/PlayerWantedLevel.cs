using Courier.Game.EventData;
using Courier.Engine.PubSubCustom;
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
            hub.Subscribe<CharcterHitEvent>(OnCharacterHit);
        }

        /// <summary>
        /// Increases the player's wanted level by a set amount.
        /// </summary>
        private void IncreasePlayerWantedLevel()
        {
            var newWantedLevel = currentWantedLevel + 1 >= MaxWantedLevel ? MaxWantedLevel : currentWantedLevel + 1;
            if (newWantedLevel != currentWantedLevel)
            {
                currentWantedLevel = newWantedLevel;
                hub.Publish(new UpdateWantedLevelEvent { NewWantedLevel = currentWantedLevel });
            }
        }

        private void OnCharacterHit(CharcterHitEvent eventData)
        {
            IncreasePlayerWantedLevel();
        }
    }
}
