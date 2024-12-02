using Courier.Engine;
using Courier.Engine.Collisions;
using Courier.Engine.Nodes;
using Courier.Game.BulletCode;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;
using PubSub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game.EnemyCode
{
    public abstract class EnemyBase : Node
    {
        protected readonly Hub hub;

        protected readonly Player player;
        protected readonly Sprite sprite;
        protected readonly CollisionNode collisionNode;

        protected readonly GameTimer shootTimer;
        protected readonly float shootRange;
        protected readonly float timeBetweenBullets;

        public EnemyState State { get; protected set; } = EnemyState.OneStar;

        /// <param name="timeBetweenBullets">The time interval between bullets fired by the enemy.</param>
        /// <param name="shootRange">The range that the player must be in for the enemy to shoot at them.</param>
        /// <param name="textureKey">The texture key for the emeny's sprite.</param>
        /// <param name="collisionRadius">The radius of the enemy's collision sphere.</param>
        public EnemyBase(
            Node parent, 
            Player player, 
            float timeBetweenBullets, 
            float shootRange,
            string textureKey,
            float collisionRadius
        ) : base(parent)
        {
            hub = Hub.Default;
            this.timeBetweenBullets = timeBetweenBullets;
            this.shootRange = shootRange;
            this.player = player;

            sprite = new Sprite(this, textureKey);
            Children.Add(sprite);

            var collisionShape = new CollisionSphere(this, collisionRadius);
            collisionNode = new CollisionNode(this, collisionShape, CollisionNodeType.Enemy);
            collisionNode.OnCollision += OnCollision;
            Children.Add(collisionNode);

            // Create the shoot timer.
            shootTimer = new GameTimer(timeBetweenBullets, TryCreateBullet);
            shootTimer.Loop = true;
            shootTimer.Start();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (State == EnemyState.Destroyed)
            {
                return;
            }

            // Tick the shootTimer.
            shootTimer.Tick(gameTime);
        }

        /// <summary>
        /// Calling this function updates the enemy's State.
        /// </summary>
        /// <param name="playerWantedLevel">The player's wanted level.</param>
        public void UpdateEnemyState(int playerWantedLevel)
        {
            if (playerWantedLevel == 3)
            {
                State = EnemyState.ThreeStar;
            } else if (playerWantedLevel == 5)
            {
                State = EnemyState.FiveStar;
            }
            UpdateShootTimerDuration();
        }

        private void OnCollision(object sender, CollisionEventArgs eventArgs)
        {
            if (eventArgs.collisionNode.CollisionNodeType == CollisionNodeType.Parcel)
            {
                State = EnemyState.Destroyed;
            }
        }

        /// <summary>
        /// Updates the shoot timer's duration based on the enemy's State.
        /// </summary>
        protected abstract void UpdateShootTimerDuration();

        /// <summary>
        /// Attempts to create a Bullet and fires it in the direction of the player.
        /// </summary>
        protected abstract void TryCreateBullet();
    }
}
