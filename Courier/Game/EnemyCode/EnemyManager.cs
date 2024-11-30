using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public class EnemyManager : Node
    {
        private readonly Hub hub;
        private readonly EnemyBase[] enemies;
        /// <summary>
        /// Boolean value used to track if an event was sent notifying all subscribers that a particular enemy was destroyed.
        /// </summary>
        private readonly bool[] hasSentDestroyedEvent;

        public EnemyManager(Node parent, List<EnemyBase> enemies) : base(parent)
        {
            this.enemies = enemies.ToArray();
            Children.AddRange(enemies);

            hasSentDestroyedEvent = new bool[this.enemies.Length];
            for (int i = 0; i < this.enemies.Length; i++)
            {
                hasSentDestroyedEvent[i] = false;
            }

            this.hub = Hub.Default;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PublishAnyEnemyDestroyedEvents();
        }

        /// <summary>
        /// Checks all enemies and publishes an event for each enemy that was destroyed and hasn't had an event published yet.
        /// </summary>
        private void PublishAnyEnemyDestroyedEvents()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                var town = enemies[i];
                if (town.State == EnemyState.Destroyed && !hasSentDestroyedEvent[i])
                {
                    hub.Publish(new CharcterDestroyedEvent());
                    hasSentDestroyedEvent[i] = true;
                }
            }
        }
    }
}
