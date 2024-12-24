using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Microsoft.Xna.Framework;
using Courier.Engine.PubSubCustom;
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
        /// Boolean values used to track enemies WasHit value on the previous Update call.
        /// </summary>
        private readonly bool[] previousWasHitValues;

        public EnemyManager(Node parent, List<EnemyBase> enemies) : base(parent)
        {
            this.enemies = enemies.ToArray();
            Children.AddRange(enemies);

            previousWasHitValues = new bool[this.enemies.Length];
            UpdatePreviousWasHitValues();

            this.hub = Hub.Default;
            this.hub.Subscribe<UpdateWantedLevelEvent>(OnWantedLevelEvent);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            PublishAnyEnemyHitEvents();
            UpdatePreviousWasHitValues();
        }

        /// <summary>
        /// Checks all enemies and publishes an event for each enemy that was hit and hasn't had an event published yet.
        /// </summary>
        private void PublishAnyEnemyHitEvents()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                var enemy = enemies[i];
                // If the enemy was not hit last frame but was hit this frame, publish the hit event. 
                if (enemy.WasHit && !previousWasHitValues[i])
                {
                    hub.Publish(new CharcterHitEvent());
                }
            }
        }

        /// <summary>
        /// Updates all the previousWasHitValues for all enemies to their current WasHitValue.
        /// </summary>
        private void UpdatePreviousWasHitValues()
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                var enemy = enemies[i];
                previousWasHitValues[i] = enemy.WasHit;
            }
        }

        private void OnWantedLevelEvent(UpdateWantedLevelEvent eventData)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                var enemy = enemies[i];
                enemy.UpdateEnemyState(eventData.NewWantedLevel);
            }
        }
    }
}
