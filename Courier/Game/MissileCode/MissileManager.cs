using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.EventData;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using Courier.Engine.PubSubCustom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.MissileCode
{
    public class MissileManager : Node
    {
        private const int MaxNumberOfMissiles = 2;
        private const int WantedLevelToStartFiringMissiles = 5;
        private const float MissileTimerDuration = 0.75f;
        private readonly Vector2 missileSpawnPositionRelativeToPlayer = new Vector2(-300, -400);

        private readonly Hub hub;

        private readonly GameTimer missileTimer;
        private readonly Player player;
        private readonly List<Missile> missiles = new List<Missile>();

        /// <summary>
        /// Keeps track of whether to fire missiles or not.
        /// </summary>
        private bool shouldFireMissiles = false;

        public MissileManager(Node parent, Player player) : base(parent)
        {
            this.player = player;

            this.missileTimer = new GameTimer(MissileTimerDuration, OnMissleTimerTimeout);
            missileTimer.Loop = true;
            missileTimer.Start();

            this.hub = Hub.Default;
            hub.Subscribe<UpdateWantedLevelEvent>(OnUpdateWantedLevel);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            missileTimer.Tick(gameTime);

            // Remove any Missiles that have been marked as ShouldDestroy
            var missilesToRemove = missiles.Where(missile => missile.ShouldDestroy);
            Children.RemoveAll(c => missilesToRemove.Contains(c));
            missiles.RemoveAll(p => missilesToRemove.Contains(p));
        }

        /// <summary>
        /// Creates a new Missile.
        /// </summary>
        private void FireMissile()
        {
            var newMissile = new Missile(this, player);
            newMissile.LocalPosition = player.GlobalPosition + missileSpawnPositionRelativeToPlayer;
            Children.Add(newMissile);
            missiles.Add(newMissile);
        }

        private void OnMissleTimerTimeout()
        {
            if (shouldFireMissiles && missiles.Count() < MaxNumberOfMissiles)
            {
                FireMissile();
                missileTimer.Start();
            }
        }

        private void OnUpdateWantedLevel(UpdateWantedLevelEvent eventData)
        {
            if (eventData.NewWantedLevel >= WantedLevelToStartFiringMissiles)
            {
                shouldFireMissiles = true; 
            }
        }
    }
}
