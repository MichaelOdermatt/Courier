using Courier.Engine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Courier.Game
{
    public class Player : CharacterBody
    {
        private Sprite sprite;
        private float ballSpeed = 100f;

        public Player()
        {
            sprite = new Sprite("ball");
            Children.Add(sprite);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // The time since Update was called last.
            float updatedBallSpeed = ballSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.Up))
            {
                Velocity = Vector2.UnitY * updatedBallSpeed * -1;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                Velocity = Vector2.UnitY * updatedBallSpeed;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                Velocity = Vector2.UnitX * updatedBallSpeed * -1;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                Velocity = Vector2.UnitX * updatedBallSpeed;
            }

            ApplyVelocity();
        }
    }
}
