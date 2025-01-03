using System;
using Courier.Engine;
using Courier.Engine.Nodes;
using Courier.Game.PlayerCode;
using Microsoft.Xna.Framework;

namespace Courier.Game.EnemyCode
{
    public class Sniper : Node
    {
        private const float shootTimerDuration = 3f;
        private const float sniperShootRange = 450f;
        private const float aimLineThickness = 1f;
        private Sprite sniperSprite;
        private Sprite aimSprite;
        private Player player;
        // TODO should I make classes subscribe to a function in gameTimer instead of passing an action?
        private GameTimer shootTimer;

        public Sniper(Node parent, Player player) : base(parent)
        {
            this.player = player;            

            sniperSprite = new Sprite(this, "Gunner");
            Children.Add(sniperSprite);

            aimSprite = new Sprite(this, "LineTextureRed");
            aimSprite.NeverCull = true;
            Children.Add(aimSprite);

            shootTimer = new GameTimer(shootTimerDuration, OnShootTimer);
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);

            shootTimer.Tick(gameTime);

            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            if (vectorToPlayer.Length() > sniperShootRange) {
                aimSprite.Visible = false;
                return;
            }
            
            if (!shootTimer.IsRunning) {
                shootTimer.Start();
            }
            aimSprite.Visible = true;
            var angleToPoint = MathF.Atan2(vectorToPlayer.Y, vectorToPlayer.X);
            aimSprite.LocalRotation = angleToPoint;
            aimSprite.Scale = new Vector2(vectorToPlayer.Length(), aimLineThickness);
        }

        private void OnShootTimer() {
            var vectorToPlayer = player.GlobalPosition - GlobalPosition;
            // If the player is out of range of the sniper, return.
            if (vectorToPlayer.Length() > sniperShootRange) {
                return;
            }

            // TODO Damage the player. Maybe use a raycast that is enabled for a frame? So that it actually uses the collision system?
        }
    }
}